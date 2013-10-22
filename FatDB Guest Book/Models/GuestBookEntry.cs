using System;
using System.Linq;
using System.Web;
using FatCloud.Client.FatDB;

namespace FatDBGuestBook.Models
{
    [Serializable]
    [FatDBContext]
    public class GuestBookEntry
    {
        public GuestBookEntry()
        {
            Date = DateTime.UtcNow.Ticks;
        }

        [FatDBKey]
        public long Date { get; set; }

        public string Comment { get; set; }
    }
}