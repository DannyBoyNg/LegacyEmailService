using System;
using System.Collections.Generic;
using System.Linq;

namespace DannyBoyNg.Services
{
    public class Attachment
    {
        private byte[] _FileData = Array.Empty<byte>();

        public string FileName { get; set; } = "attachedFile";
        public IReadOnlyCollection<byte> FileData
        {
            get => _FileData;
            set => _FileData = value.ToArray();
        }
    }
}
