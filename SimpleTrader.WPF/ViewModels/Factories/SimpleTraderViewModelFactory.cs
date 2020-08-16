using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelFactory : ISimpleTraderViewModelFactory
    {
        private readonly IMajorIndexService _majorIndexService;
        private readonly BuyViewModel _buyViewModel;

        public SimpleTraderViewModelFactory(IMajorIndexService majorIndexService, BuyViewModel buyViewModel)
        {
            _majorIndexService = majorIndexService;
            _buyViewModel = buyViewModel;
        }

        public HomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel(CreateMajorIndexListingViewModel());
        }
        public MajorIndexListingViewModel CreateMajorIndexListingViewModel()
        {
            return MajorIndexListingViewModel.LoadMajorIndexViwModel(_majorIndexService);
        }

        public PortfolioViewModel CreatePortfolioViewModel()
        {
            return new PortfolioViewModel();
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return CreateHomeViewModel();
                case ViewType.Portfolio:
                    return CreatePortfolioViewModel();
                case ViewType.Buy:
                    return _buyViewModel;
                default:
                    throw new ArgumentException("The ViewType doest not have a ViewModel", "viewType");
            }
        }
    }
}
