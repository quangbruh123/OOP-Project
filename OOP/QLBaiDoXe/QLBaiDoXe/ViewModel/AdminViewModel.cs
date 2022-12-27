﻿using HandyControl.Tools.Command;
using QLBaiDoXe.ViewUserControl.SwitchViewClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLBaiDoXe.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public ICommand SelectViewCommand { get; set; }
        public AdminViewModel() 
        {
            SelectViewCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SelectView(p); });

        }
        private BaseViewModel _selectedViewModel = new SwitchViewDSNhanVien();
        public BaseViewModel SelectedViewModel
        {

            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        void SelectView(Button listViewItem)
        {
            if (listViewItem != null && listViewItem.Tag != null)
            {
                string tag = listViewItem.Tag.ToString();

                switch (tag)
                {
                    case "DanhSach":
                        {
                            SelectedViewModel = new SwitchViewDSNhanVien();
                            break;
                        }
                    case "ChamCong":
                        {
                            SelectedViewModel = new SwitchViewBangChamCong();
                            break;
                        }
                    
                    default:
                        break;
                }

            }
        }

    }
}