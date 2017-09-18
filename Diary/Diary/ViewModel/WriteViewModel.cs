using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class WriteViewModel : BindableBase
    {
        private Posts m_Post;
        public Posts Post
        {
            get
            {
                if (m_Post == null)
                {
                    m_Post = new Posts();
                }
                return m_Post;
            }
            set
            {
                m_Post = value;
                RaisePropertyChanged("Post");
            }
        }


        public DelegateCommand SaveCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {
                    Post.SaveDiary();
                });
            }
        }
    }
}
