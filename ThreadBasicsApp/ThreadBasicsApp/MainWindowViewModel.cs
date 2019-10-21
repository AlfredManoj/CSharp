using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBasicsApp
{
    class MainWindowViewModel : PropertyChangedBase
    {
        public MainWindowViewModel()
        {
            Initialise();
        }

        private void Initialise()
        {
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
            MyListCollection.Add("Hello");
        }

        private ObservableCollection<string> m_myListCollection = new ObservableCollection<string>();

        public ObservableCollection<string> MyListCollection
        {
            get
            {
                return m_myListCollection;
            }
            set
            {
                Set(ref m_myListCollection, value);
            }
        }

        public async void ThreadMethod()
        {
            MyListCollection.Add("Hi");
            Debug.WriteLine($"Thread Method Thread Before Await: {Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine($"Thread Method Thread Before Await: {Thread.CurrentThread.IsBackground}");
            Debug.WriteLine($"Thread Method Thread Before Await: {Thread.CurrentThread.IsThreadPoolThread}");
            await Task.Run(() => TaskMethod());
            Debug.WriteLine($"Thread Method Thread After Await: {Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine($"Thread Method Thread Before Configure Await: {Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine($"Thread Method Thread Before Configure Await: {Thread.CurrentThread.IsBackground}");
            Debug.WriteLine($"Thread Method Thread Before Configure Await: {Thread.CurrentThread.IsThreadPoolThread}");
            // the synchronization context makes sure that the thread which calls the await methof cntinues with the 
            // execution after the resut arrives. That means if thread A cas a await method, then the method gets executed on Thread B (say)
            // but the synchronization context in .net will mark that the remaining steps on the flow should be executed on A. (but this 
            // will not block thread A. It will continue other operations until the result is ready. In short it will be in queue list of tasks
            // that A needs to execute). But if we tell that we don't need to have the same thread to execute, by setting it to false, then
            // it will not label as the remaining should be executed in thread A. For example here, the await is called from the Ui thread.
            // Since we tell that there is no need to await, the remaining steps after the await will not be executed in the Ui Thread,
            // which will throw an exception here.
            await Task.Delay(2000).ConfigureAwait(false);
            Debug.WriteLine($"Thread Method Thread After Configure Await: {Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine($"Thread Method Thread After Configure Await: {Thread.CurrentThread.IsBackground}");
            Debug.WriteLine($"Thread Method Thread After Configure Await: {Thread.CurrentThread.IsThreadPoolThread}");
            MyListCollection.Add("Test");
        }

        private void TaskMethod()
        {
            Debug.WriteLine($"Thread Method Thread Before Delay: {Thread.CurrentThread.ManagedThreadId}");
            Debug.WriteLine($"Thread Method Thread Before Delay: {Thread.CurrentThread.IsBackground}");
            Debug.WriteLine($"Thread Method Thread Before Delay: {Thread.CurrentThread.IsThreadPoolThread}");

            App.Current.Dispatcher.BeginInvoke(new System.Action(() =>
            {
                Debug.WriteLine($"OnUi Task Delegation Thread: {Thread.CurrentThread.ManagedThreadId}");
                Debug.WriteLine($"OnUi Task Delegation Thread: {Thread.CurrentThread.IsBackground}");
                Debug.WriteLine($"OnUi Task Delegation Thread: {Thread.CurrentThread.IsThreadPoolThread}");
                MyListCollection.Add("Hi");
            }));

            Debug.WriteLine($"Thread Method Thread After Delay: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
