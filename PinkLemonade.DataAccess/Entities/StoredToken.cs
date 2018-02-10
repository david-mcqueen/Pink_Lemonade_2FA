using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PinkLemonade.DataAccess.Entities
{
    [Table("stored_token")]
    public class StoredToken
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string TokenRaw { get; set; }
        public string Secret { get; set; }
        public string Label { get; set; }
        public string Issuer { get; set; }
        
    }
}
