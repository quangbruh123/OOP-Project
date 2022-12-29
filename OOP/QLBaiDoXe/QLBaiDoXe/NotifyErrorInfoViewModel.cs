using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace QLBaiDoXe
{
    public class NotifyErrorInfoViewModel : ObservableObject, INotifyDataErrorInfo
    {
        //Khởi tạo loại textbox tương ứng trong đây:

        private string tenNguoiDung;
        public string TenNguoiDung
        {
            get => tenNguoiDung;
            set => SetProperty(ref tenNguoiDung, value);
        }

        private string cccd;
        public string CCCD
        {
            get => cccd;
            set => SetProperty(ref cccd, value);
        }

        private string chucVu;
        public string ChucVu
        {
            get => chucVu;
            set => SetProperty(ref chucVu, value);
        }

        private string taiKhoan;
        public string TaiKhoan
        {
            get => taiKhoan;
            set => SetProperty(ref taiKhoan, value);
        }

        private string matKhau;
        public string MatKhau
        {
            get => matKhau;
            set => SetProperty(ref matKhau, value);
        }

       
        public RelayCommand SubmitCommand { get; }

        public NotifyErrorInfoViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
            //Doing this will cause the errors to show immediately
            Validate(nameof(TenNguoiDung));
            Validate(nameof(CCCD));
            Validate(nameof(ChucVu));
            Validate(nameof(TaiKhoan));
            Validate(nameof(MatKhau));

        }

        private bool CanSubmit()
        {
            //Link the CanExecute state of the command to the visible errors on the screen. 
            //You can also separate the command from the validation errors and simply change this to match the one in SimpleViewModel.CanSubmit
            return !HasErrors;
        }

        private void OnSubmit()
        {
            Debug.WriteLine("Form Submitted");
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            Validate(e.PropertyName);
        }

        private void Validate(string changedPropertyName)
        {
            //Do whatever validation is needed here
            //You can do validation accross multiple properties as well.
            switch (changedPropertyName)
            {
                case nameof(TenNguoiDung):
                    if (string.IsNullOrWhiteSpace(TenNguoiDung))
                    {
                        _ValidationErrorsByProperty[nameof(TenNguoiDung)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(TenNguoiDung)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(TenNguoiDung)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(TenNguoiDung)));
                    }
                    break;

                case nameof(CCCD):
                    if (string.IsNullOrWhiteSpace(CCCD))
                    {
                        _ValidationErrorsByProperty[nameof(CCCD)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(CCCD)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(CCCD)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(CCCD)));
                    }
                    break;

                case nameof(ChucVu):
                    if (string.IsNullOrWhiteSpace(ChucVu))
                    {
                        _ValidationErrorsByProperty[nameof(ChucVu)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ChucVu)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(ChucVu)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ChucVu)));
                    }
                    break;
              

                case nameof(TaiKhoan):
                    if (string.IsNullOrWhiteSpace(TaiKhoan))
                    {
                        _ValidationErrorsByProperty[nameof(TaiKhoan)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(TaiKhoan)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(TaiKhoan)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(TaiKhoan)));
                    }
                    break;

                case nameof(MatKhau):
                    if (string.IsNullOrWhiteSpace(MatKhau))
                    {
                        _ValidationErrorsByProperty[nameof(MatKhau)] = new List<object> { " Trường dữ liệu bắt buộc." };
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(MatKhau)));
                    }
                    else if (_ValidationErrorsByProperty.Remove(nameof(MatKhau)))
                    {
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(MatKhau)));
                    }
                    break;

                
            }
            SubmitCommand.NotifyCanExecuteChanged();

            //Notify the command that it needs to invoke its CanExecute method (in this case CanSubmit).

            /*if (string.IsNullOrWhiteSpace(changedPropertyName))
            {
                _ValidationErrorsByProperty[nameof(changedPropertyName)] = new List<object> { " Trường dữ liệu bắt buộc." };
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(changedPropertyName)));
            }
            else if (_ValidationErrorsByProperty.Remove(nameof(changedPropertyName)))
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(changedPropertyName)));
            }
            */
        }
    

        public IEnumerable GetErrors(string propertyName)
        {
            if (_ValidationErrorsByProperty.TryGetValue(propertyName, out List<object> errors))
            {
                return errors;
            }
            return Array.Empty<object>();
        }

        private readonly Dictionary<string, List<object>> _ValidationErrorsByProperty =
            new Dictionary<string, List<object>>();

        public bool HasErrors => _ValidationErrorsByProperty.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}