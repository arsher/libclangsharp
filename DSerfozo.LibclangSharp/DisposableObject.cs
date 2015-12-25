using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp
{
    public abstract class DisposableObject<T> : IDisposable where T : class
    {
        private readonly Action<T, bool> disposeCallback;
        private bool disposed;

        protected internal bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        protected DisposableObject(Action<T, bool> disposeCallback)
        {
            this.disposeCallback = disposeCallback;
        }

        ~DisposableObject()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("The current instance has been disposed.");
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposeCallback != null)
                {
                    disposeCallback(this as T, disposing);
                }

                disposed = true;
            }
        }
    }
}
