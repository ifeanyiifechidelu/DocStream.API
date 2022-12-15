using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.FileEntities
{
    [Table("FileDetails")]
    public class FileDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public FileType FileType { get; set; }
        [ForeignKey(nameof(ApplicantId))]
        public int ApplicantId { get; set; }
        
    }
}
