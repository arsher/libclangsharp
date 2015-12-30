using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSerfozo.LibclangSharp
{
    public abstract class DisposableObject : IDisposable
    {
        private bool disposed;

        protected internal bool IsDisposed
        {
            get
            {
                return disposed;
            }
        }

        protected DisposableObject()
        {
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

        protected virtual void DisposeInternal(bool disposing)
        {
            
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                DisposeInternal(disposing);

                disposed = true;
            }
        }
    }
}
