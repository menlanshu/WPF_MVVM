using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private IMajorIndexService _majorIndexService;

        private MajorIndex _dowJones;

        public MajorIndex DowJones
        {
            get { return _dowJones; }
            set
            {
                _dowJones = value;
                OnPropertyChanged(nameof(DowJones));
            }
        }

        private MajorIndex _nasdaq;

        public MajorIndex Nasdaq
        {
            get { return _nasdaq; }
            set
            {
                _nasdaq = value;
                OnPropertyChanged(nameof(Nasdaq));
            }
        }

        private MajorIndex _sp500;

        public MajorIndex SP500
        {
            get { return _sp500; }
            set
            {
                _sp500 = value;
                OnPropertyChanged(nameof(SP500));
            }
        }

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexViwModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);
            // Don't want to wait, I want to get the major index as soon as possible
            //majorIndexViewModel.LoadMajorIndex();
            majorIndexViewModel.LoadMajorIndexV2();

            return majorIndexViewModel;
        }

        private async Task LoadMajorIndex()
        {
            DowJones = await _majorIndexService.GetMajorIndex(MajorIndexType.DowJones);
            Nasdaq = await _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq);
            SP500 = await _majorIndexService.GetMajorIndex(MajorIndexType.SP500);
        }

        private void LoadMajorIndexV2()
        {
            //Continue with will do a callback
            _majorIndexService.GetMajorIndex(MajorIndexType.DowJones).ContinueWith(task =>
           {
                // if exception is null, then we have no exception
                if (task.Exception == null)
               {
                   DowJones = task.Result;
               }
           });

            _majorIndexService.GetMajorIndex(MajorIndexType.Nasdaq).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Nasdaq = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.SP500).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    SP500 = task.Result;
                }
            });
        }

    }
}
