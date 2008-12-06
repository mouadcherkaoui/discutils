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
using System.IO;

namespace DiscUtils.Fat
{
    internal class FatFileInfo : DiscFileInfo
    {
        private FatFileSystem _fileSystem;
        private string _path;

        public FatFileInfo(FatFileSystem fileSystem, string path)
        {
            if (string.IsNullOrEmpty(path) || path.EndsWith("\\", StringComparison.Ordinal))
            {
                throw new ArgumentException("Invalid file path", "path");
            }

            _fileSystem = fileSystem;
            _path = path.TrimStart('\\');
        }

        public override void CopyTo(string destinationFileName, bool overwrite)
        {
            _fileSystem.CopyFile(_path, destinationFileName, overwrite);
        }

        public override long Length
        {
            get { return GetDirEntry().FileSize; }
        }

        public override Stream Open(FileMode mode)
        {
            return _fileSystem.OpenFile(_path, mode);
        }

        public override Stream Open(FileMode mode, FileAccess access)
        {
            return _fileSystem.OpenFile(_path, mode, access);
        }

        public override void MoveTo(string destinationFileName)
        {
            _fileSystem.MoveFile(_path, destinationFileName);
        }

        public override string Name
        {
            get { return Utilities.GetFileFromPath(_path); }
        }

        public override string FullName
        {
            get { return _path; }
        }

        public override FileAttributes Attributes
        {
            get { return _fileSystem.GetAttributes(_path); }
            set { _fileSystem.SetAttributes(_path, value); }
        }

        public override DiscDirectoryInfo Parent
        {
            get { return new FatDirectoryInfo(_fileSystem, Utilities.GetDirectoryFromPath(_path)); }
        }

        public override bool Exists
        {
            get { return _fileSystem.FileExists(_path); }
        }

        public override DateTime CreationTime
        {
            get { return _fileSystem.GetCreationTime(_path); }
            set { _fileSystem.SetCreationTime(_path, value); }
        }

        public override DateTime CreationTimeUtc
        {
            get { return _fileSystem.GetCreationTimeUtc(_path); }
            set { _fileSystem.SetCreationTimeUtc(_path, value); }
        }

        public override DateTime LastAccessTime
        {
            get { return _fileSystem.GetLastAccessTime(_path); }
            set { _fileSystem.SetLastAccessTime(_path, value); }
        }

        public override DateTime LastAccessTimeUtc
        {
            get { return _fileSystem.GetLastAccessTimeUtc(_path); }
            set { _fileSystem.SetLastAccessTimeUtc(_path, value); }
        }

        public override DateTime LastWriteTime
        {
            get { return _fileSystem.GetLastWriteTime(_path); }
            set { _fileSystem.SetLastWriteTime(_path, value); }
        }

        public override DateTime LastWriteTimeUtc
        {
            get { return _fileSystem.GetLastWriteTimeUtc(_path); }
            set { _fileSystem.SetLastWriteTimeUtc(_path, value); }
        }

        public override void Delete()
        {
            _fileSystem.DeleteFile(_path);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            FatFileInfo other = (FatFileInfo)obj;

            return _fileSystem == other._fileSystem && _path == other._path;
        }

        public override int GetHashCode()
        {
            return _fileSystem.GetHashCode() ^ _path.GetHashCode();
        }

        private DirectoryEntry GetDirEntry()
        {
            DirectoryEntry dirEntry = _fileSystem.GetDirectoryEntry(_path);
            if (dirEntry == null)
            {
                throw new FileNotFoundException("File not found", _path);
            }
            return dirEntry;
        }
    }
}