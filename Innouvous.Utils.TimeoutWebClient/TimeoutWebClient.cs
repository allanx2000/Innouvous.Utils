using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Innouvous.Utils
{
    /// <summary>
    /// Wrapper for System.Net.WebClient with support for Synchronous Query Timeout
    /// </summary>
    public class TimeoutWebClient
    {
        /// <summary>
        /// Timeout, in minutes
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Frequency of result check, in seconds
        /// </summary>
        public int CheckFrequency {get; private set;}

        private WebClient client;

        private volatile Exception lastException = null;

        public WebClient GetWebClient()
        {
            return client;
        }

        /// <summary>
        /// Gets the last exception
        /// </summary>
        /// <returns>The last exception or null</returns>
        public Exception GetLastException()
        {
            return lastException;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout">Timeout, in minutes</param>
        /// <param name="checkFrequecy">Frequency to check for results, in seconds</param> 
        public TimeoutWebClient(WebClient wc, int timeout = 5, int checkFrequecy = 20)
        {
            this.client = wc;

            this.Timeout = timeout;
            this.CheckFrequency = checkFrequecy;
        }

        public byte[] DownloadData(string url)
        {
            byte[] result = null;
            Thread th = new Thread(() =>
            {
                try
                {
                    result = client.DownloadData(url);
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            });
            
            DoDownload(th);

            return result;

        }

        public void DownloadFile(string url, string outFile)
        {
            Thread th = new Thread(() =>
            {
                try
                {
                    client.DownloadFile(url, outFile);
                }
                catch (Exception e)
                {
                    lastException = e;
                }
            });
            
            DoDownload(th);
        }

        public string DownloadString(string url)
        {
            string result = null;
            
            Thread th = new Thread(() =>
                {
                    try
                    {
                        result = client.DownloadString(url);
                    }
                    catch (Exception e)
                    {
                        lastException = e;
                    }
                });
            DoDownload(th);

            return result;

        }

        private void DoDownload(Thread actionThread)
        {
            lastException = null;

            DateTime expireTime = DateTime.Now.AddMinutes(Timeout);

            actionThread.Start();

            while (DateTime.Now < expireTime)
            {
                if (lastException != null)
                    throw lastException;
                else if (actionThread.ThreadState == ThreadState.Stopped)
                    break;
                else 
                    Thread.Sleep(CheckFrequency * 1000);
            }

            actionThread.Abort();
            throw new Exception("Client timed out");

        }
    }
}
