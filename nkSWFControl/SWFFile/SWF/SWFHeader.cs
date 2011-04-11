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
//using DevelopDotNet.Compression;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;

namespace SWFFile.SWF
{

    public class SWFHeader
    {
        private int _version;
        private int _fileLength;
        private int _frameMinX;
        private int _frameMinY;
        private int _frameMaxX;
        private int _frameMaxY;

        private int _frameRate;
        private int _frameCount;

        public int Version { get { return _version; } }
        public int FileLength { get { return _fileLength; } }
        public int FrameWidth { get { return _frameMaxX / 20; } }
        public int FrameHeight { get { return _frameMaxY / 20; } }
        public int FrameRate { get { return _frameRate/256; } }
        public int FrameCount { get { return _frameCount; } }

        public SWFHeader(ref Stream stream,bool compressed)
        {
            //version
            _version = stream.ReadByte();

            //FileLength 
            _fileLength = stream.ReadByte();
            _fileLength += stream.ReadByte()<<8;
            _fileLength += stream.ReadByte()<<16;
            _fileLength += stream.ReadByte()<<24;

            if (compressed)
            {
                //decompress
                MemoryStream mstream = new MemoryStream();
                System.IO.Compression.GZipStream zstream = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress);
                //ZStream zstream = new ZStream(stream,CompressionMode.Decompress);
                
                int count;
                byte[] buff = new byte[256];
                do
                {
                    count = zstream.Read(buff, 0, buff.Length);
                    mstream.Write(buff, 0, count);

                } while (count > 0);
                mstream.Position = 0;
                stream.Close();
                stream = mstream;
            }

            //Framesize
            int first = stream.ReadByte();
            int nBits = first >> 3;
            int nRectBytes = (int) Math.Ceiling( (nBits * 4 - 3) / 8.0f);
            byte[] rectbytes = new byte[nRectBytes+1];
            rectbytes[0]=(byte)first;
            stream.Read(rectbytes, 1, nRectBytes);
            _frameMinX = BitReader(rectbytes,5,nBits,0);
            _frameMaxX = BitReader(rectbytes, 5, nBits, 1);
            _frameMinY = BitReader(rectbytes, 5, nBits, 2);
            _frameMaxY = BitReader(rectbytes, 5, nBits, 3);
            

            //framerate
            _frameRate = stream.ReadByte();
            _frameRate += stream.ReadByte() << 8;

            //framecount
            _frameCount = stream.ReadByte() ;
            _frameCount += stream.ReadByte() << 8;

            return;
        }

        private int BitReader(byte[] rectbytes, int offset, int nBits, int item)
        {
            int value=0;
            int BitStart = offset + nBits * item;
            int BitEnd = offset + nBits * (item+1);
            
            //this is not that efficient, but pretty clear
            for (int i = BitStart ; i < BitEnd ; i++)
            {
                int bit = rectbytes[i / 8] >> (7-i%8);
                bit &= 1;
                value = value<<1;
                value +=bit;
                
                
            }
            return value;
        }
    }
}
