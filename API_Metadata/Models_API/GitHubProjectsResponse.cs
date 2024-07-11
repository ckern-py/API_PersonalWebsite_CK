using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Metadata.Models_API
{
    public  class GitHubProjectsResponse : BaseResponse
    {
        public string ImageSource { get; set; }

        public string ImageAlt { get; set; }

        public string SectionTitle { get; set; }

        public string SectionDescription { get; set; }

        public string RepoLink { get; set; }
    }
}
