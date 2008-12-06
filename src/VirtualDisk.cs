﻿//
// Copyright (c) 2008, Kenneth Bell
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.ObjectModel;
using System.IO;
using DiscUtils.Partitions;

namespace DiscUtils
{
    /// <summary>
    /// Base class representing virtual hard disks.
    /// </summary>
    public abstract class VirtualDisk : IDisposable
    {
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        ~VirtualDisk()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of this instance, freeing underlying resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of underlying resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> if running inside Dispose(), indicating
        /// graceful cleanup of all managed objects should be performed, or <c>false</c>
        /// if running inside destructor.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets the geometry of the disk.
        /// </summary>
        public abstract Geometry Geometry
        {
            get;
        }

        /// <summary>
        /// Gets the content of the disk as a stream.
        /// </summary>
        public abstract Stream Content
        {
            get;
        }

        /// <summary>
        /// Gets the layers that make up the disk.
        /// </summary>
        public abstract ReadOnlyCollection<VirtualDiskLayer> Layers
        {
            get;
        }

        /// <summary>
        /// Gets the object that interprets the partition structure.
        /// </summary>
        public virtual PartitionTable Partitions
        {
            get
            {
                return new BiosPartitionTable(this);
            }
        }
    }
}