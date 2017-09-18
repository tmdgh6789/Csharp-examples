using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class Posts : BindableBase
    {
        private int m_Num;
        public int Num
        {
            get
            {
                return m_Num;
            }
            set
            {
                m_Num = value;
                RaisePropertyChanged("Num");
            }
        }

        private string m_Title;
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                RaisePropertyChanged("Title");
            }
        }


        private string m_Contents;
        public string Contents
        {
            get
            {
                return m_Contents;
            }
            set
            {
                m_Contents = value;
                RaisePropertyChanged("Contents");
            }
        }


        private DateTime m_CreateDate;
        public DateTime CreateDate
        {
            get
            {
                return m_CreateDate;
            }
            set
            {
                m_CreateDate = value;
                RaisePropertyChanged("CreateDate");
            }
        }


        private DateTime m_LastUpdate;
        public DateTime LastUpdate
        {
            get
            {
                return m_LastUpdate;
            }
            set
            {
                m_LastUpdate = value;
                RaisePropertyChanged("LastUpdate");
            }
        }
        
        /// <summary>
        /// 디비에 저장
        /// </summary>
        public void SaveDiary()
        {

        }
    }
}
