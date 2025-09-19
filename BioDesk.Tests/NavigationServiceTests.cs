using Xunit;
using FluentAssertions;
using BioDesk.App.Services;

namespace BioDesk.Tests;

public class NavigationServiceTests
{
    [Fact]
    public void NavigationService_ShouldStartWithHomeView()
    {
        // Arrange
        var navigationService = new NavigationService();

        // Act & Assert
        navigationService.CurrentView.Should().Be("Home");
    }

    [Fact]
    public void NavigationService_ShouldNavigateToValidViews()
    {
        // Arrange
        var navigationService = new NavigationService();

        // Act & Assert
        navigationService.GoTo("Pacientes");
        navigationService.CurrentView.Should().Be("Pacientes");

        navigationService.GoTo("IrisAnonima");
        navigationService.CurrentView.Should().Be("IrisAnonima");

        navigationService.GoTo("Home");
        navigationService.CurrentView.Should().Be("Home");
    }

    [Fact]
    public void NavigationService_ShouldIgnoreInvalidViews()
    {
        // Arrange
        var navigationService = new NavigationService();
        var initialView = navigationService.CurrentView;

        // Act
        navigationService.GoTo("InvalidView");

        // Assert
        navigationService.CurrentView.Should().Be(initialView);
    }
}