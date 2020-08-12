using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexViewModel
    {
        private IMajorIndexService _majorIndexService;

        public MajorIndex DowJones { get; set; }
        public MajorIndex Nasdaq { get; set; }
        public MajorIndex SP500 { get; set; }

        public MajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexViewModel LoadMajorIndexViwModel(IMajorIndexService majorIndexService)
        {
            MajorIndexViewModel majorIndexViewModel = new MajorIndexViewModel(majorIndexService);
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
