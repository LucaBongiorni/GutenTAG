using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GutenTAGPlugin
{
    public interface IGutenTAGPlugin
    {
        IGutenTAG GutenTAGHost { get; set; }
        void Show();
        string ToString();
    }
    public interface IGutenTAG
    {
        byte[] getTagBlock(byte block);
        void writeTagBlock(byte block, byte[] data);
        byte[] getTagUID();
        bool isTagPresent();
        void closePlugin();
    }
}
