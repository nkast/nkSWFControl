/*
Copyright (c) 2010 Kastellanos Nikos

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SWFFile.SWF
{
    public class SWFReader: IDisposable
    {
        private Stream input;
        readonly string Filename;
        private bool _compressed;
        private SWFHeader header;
        public SWFHeader Header {get {return header;}}

        public SWFReader(string filename)
        {
            Filename=filename;
            input = File.OpenRead(filename);
            header = ReadHeader();
            return;
        }

        protected SWFHeader ReadHeader()
        {
            // check signature
            byte[] b = new byte[3];
            int c = input.Read(b, 0, 3);
            if (c != 3) return null;

            _compressed = false;
            if (b[0] == 'C' && b[1] == 'W' && b[2] == 'S')
            {   // file is compressed
                _compressed = true;
            }
            else
                if (!(b[0] == 'F' && b[1] == 'W' && b[2] == 'S')) return null;

            SWFHeader header = new SWFHeader(ref input, _compressed);

            return header;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (input != null)
            {
                input.Close();
                input = null;
            }
        }

        #endregion
    }
}
