using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.AppLogic.Enums;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels;

public partial class UserAnalyticsPageViewModel : BaseViewModel
{
    public ObservableCollection<GenderDataPoint> GenderData { get; private set; } = [];
    public ObservableCollection<AgeDataPoint> AgeData { get; private set; } = [];

    private readonly UserManagePageViewModel _userViewModel;

    public UserAnalyticsPageViewModel(UserManagePageViewModel userManagePageViewModel)
    {
        Title = "User Analytics";
        _userViewModel = userManagePageViewModel;
        
        // Subscribe to collection changes
        _userViewModel.Users.CollectionChanged += Users_CollectionChanged;
        
        LoadData();
    }

    private void Users_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {
        if (_userViewModel == null) return;

        // Calculate distribution from real data
        var users = _userViewModel.Users;
        
        if (users.Count == 0)
        {
             // Optionally show empty state or keep old data
             GenderData.Clear();
             AgeData.Clear();
             return;
        }

        // Gender Distribution
        var genderCounts = users.GroupBy(u => u.Gender)
                                .Select(g => new GenderDataPoint(g.Key, g.Count()))
                                .ToList();

        GenderData.Clear();
        foreach (var item in genderCounts)
        {
            GenderData.Add(item);
        }

        // Age Distribution
        var ageGroups = new Dictionary<string, int>
        {
            { "0-18", 0 },
            { "19-30", 0 },
            { "31-50", 0 },
            { "51+", 0 }
        };

        foreach (var user in users)
        {
            if (user.Age <= 18) ageGroups["0-18"]++;
            else if (user.Age <= 30) ageGroups["19-30"]++;
            else if (user.Age <= 50) ageGroups["31-50"]++;
            else ageGroups["51+"]++;
        }

        AgeData.Clear();
        foreach (var group in ageGroups)
        {
            AgeData.Add(new AgeDataPoint(group.Key, group.Value));
        }
    }

    [RelayCommand]
    async Task OpenChartOptions(string chartTitle)
    {
        await Application.Current.MainPage.DisplayActionSheet($"Options for {chartTitle}", "Cancel", null, "View Details", "Export Data");
    }
}

public class GenderDataPoint
{
    public string GenderLabel { get; }
    public double Count { get; }

    public GenderDataPoint(Gender gender, double count)
    {
        GenderLabel = gender.ToString();
        Count = count;
    }
}

public class AgeDataPoint
{
    public string AgeGroup { get; }
    public double Count { get; }

    public AgeDataPoint(string ageGroup, double count)
    {
        AgeGroup = ageGroup;
        Count = count;
    }
}
