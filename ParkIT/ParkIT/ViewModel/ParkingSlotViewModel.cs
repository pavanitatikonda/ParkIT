using ParkIT.APIService;
using ParkIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkIT.ViewModel
{
    public class ParkingSlotViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _slotNo;
        public string SlotNo
        {
            get
            {
                return _slotNo;
            }
            set
            {
                _slotNo = value;
                OnPropertyChanged();
            }
        }
        private int _inputPin;
        public int InputPin
        {
            get
            {
                return _inputPin;
            }
            set
            {
                _inputPin = value;
                OnPropertyChanged();
            }
        }
        private int _status;
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ParkingSlot> _listslots;
        public ObservableCollection<ParkingSlot> ListSLots
        {
            get
            {
                return _listslots;
            }
            set
            {
                _listslots = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ParkingSlotViewModel()
        {
            //LoadData();
        }

        public async Task<ObservableCollection<ParkingSlot>> LoadData()
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                ApiService apiService = new ApiService("http://parkitapp.azurewebsites.net");
                ObservableCollection<ParkingSlot> ApiResult = await apiService.Get<ObservableCollection<ParkingSlot>>("/api/values/GetDBValues").ConfigureAwait(false);

                ObservableCollection<ParkingSlot> _listParking = new ObservableCollection<ParkingSlot>();
                //ParkingSlot _parkingArea = new ParkingSlot
                //{
                //    ID = 1,
                //    InputPin = 1,
                //    SlotNo = 1,
                //    Status = "booked"
                //};
                //ParkingSlot _parkingArea1 = new ParkingSlot
                //{
                //    ID = 1,
                //    InputPin = 1,
                //    SlotNo = 1,
                //    Status = "booked"
                //};
                //ParkingSlot _parkingArea2 = new ParkingSlot
                //{
                //    ID = 2,
                //    InputPin = 2,
                //    SlotNo = 2,
                //    Status = "booked"
                //};
                //ParkingSlot _parkingArea3 = new ParkingSlot
                //{
                //    ID = 3,
                //    InputPin = 3,
                //    SlotNo = 3,
                //    Status = "booked"
                //};
                //ParkingSlot _parkingArea4 = new ParkingSlot
                //{
                //    ID = 4,
                //    InputPin = 4,
                //    SlotNo = 4,
                //    Status = "booked"
                //};
                //ParkingSlot _parkingArea5 = new ParkingSlot
                //{
                //    ID = 5,
                //    InputPin = 5,
                //    SlotNo = 5,
                //    Status = "booked"
                //};
                //_listParking.Add(_parkingArea1);
                //_listParking.Add(_parkingArea2);
                //_listParking.Add(_parkingArea3);
                //_listParking.Add(_parkingArea4);
                //_listParking.Add(_parkingArea5);

                ListSLots = ApiResult;
            });
            return ListSLots;
        }
    }
}
