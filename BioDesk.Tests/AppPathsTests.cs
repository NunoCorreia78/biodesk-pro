using Xunit;
using FluentAssertions;
using BioDesk.App.Services;

namespace BioDesk.Tests;

public class AppPathsTests
{
    [Fact]
    public void AppPaths_ShouldHaveValidBasePaths()
    {
        // Act & Assert
        AppPaths.BaseDir.Should().NotBeNullOrEmpty();
        AppPaths.Images.Should().NotBeNullOrEmpty();
        AppPaths.Logo.Should().NotBeNullOrEmpty();
        AppPaths.Util.Should().NotBeNullOrEmpty();
        AppPaths.Assets.Should().NotBeNullOrEmpty();
        AppPaths.Templates.Should().NotBeNullOrEmpty();
        AppPaths.Consentimentos.Should().NotBeNullOrEmpty();
        AppPaths.Logs.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void AppPaths_ShouldUseCorrectSubdirectories()
    {
        // Act & Assert
        AppPaths.Images.Should().EndWith("images_interface");
        AppPaths.Logo.Should().EndWith("logo.png");
        AppPaths.Assets.Should().EndWith("assets");
        AppPaths.Templates.Should().EndWith("templates");
        AppPaths.Consentimentos.Should().EndWith("consentimentos");
        AppPaths.Logs.Should().EndWith("logs");
    }
}