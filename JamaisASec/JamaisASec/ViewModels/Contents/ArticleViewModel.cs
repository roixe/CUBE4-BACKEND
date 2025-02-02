using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamaisASec.Models;

namespace JamaisASec.ViewModels.Contents
{
    public class ArticleViewModel(Article article) : BaseViewModel
    {
        public Article Article { get; } = article;
    }
}
