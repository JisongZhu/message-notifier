using System;
using System.Collections.Generic;
using System.Text;

namespace TaskbarNotifier
{
    public class MessageModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string CreateTime { get; set; }

        public int HasRead { get; set; }
    }
}
