using Chinook.Web.Models;
using Chinook.Web.ViewModels;

namespace Chinook.Web.Tests;

/// <summary>
/// Tests for ArtistViewModel construction, album count accuracy, sorting, search filtering,
/// empty results, and null/edge-case data validation.
/// </summary>
public class ArtistViewModelTests
{
    // ── Construction ──────────────────────────────────────────────────────────

    [Fact]
    public void FromArtist_MapsIdAndName()
    {
        var artist = new Artist { ArtistId = 1, Name = "AC/DC" };
        var vm = ArtistViewModel.FromArtist(artist);

        Assert.Equal(1, vm.ArtistId);
        Assert.Equal("AC/DC", vm.Name);
    }

    [Fact]
    public void FromArtist_CountsAlbumsCorrectly_WhenArtistHasMultipleAlbums()
    {
        var artist = new Artist
        {
            ArtistId = 2,
            Name = "The Beatles",
            Albums =
            {
                new Album { AlbumId = 1, Title = "Abbey Road", ArtistId = 2 },
                new Album { AlbumId = 2, Title = "Let It Be", ArtistId = 2 }
            }
        };

        var vm = ArtistViewModel.FromArtist(artist);

        Assert.Equal(2, vm.AlbumCount);
    }

    [Fact]
    public void FromArtist_AlbumCount_IsZero_WhenArtistHasNoAlbums()
    {
        var artist = new Artist { ArtistId = 3, Name = "Solo Artist" };
        var vm = ArtistViewModel.FromArtist(artist);

        Assert.Equal(0, vm.AlbumCount);
    }

    [Fact]
    public void FromArtist_Name_IsEmptyString_WhenArtistNameIsNull()
    {
        var artist = new Artist { ArtistId = 4, Name = null };
        var vm = ArtistViewModel.FromArtist(artist);

        Assert.Equal(string.Empty, vm.Name);
    }

    // ── Search filtering ──────────────────────────────────────────────────────

    [Theory]
    [InlineData("AC", true)]
    [InlineData("ac", true)]         // case-insensitive
    [InlineData("AC/DC", true)]      // exact match
    [InlineData("dc", true)]         // substring
    [InlineData("Beatles", false)]   // no match
    public void MatchesSearch_ArtistName_ReturnsExpected(string term, bool expected)
    {
        var vm = new ArtistViewModel { ArtistId = 1, Name = "AC/DC" };
        Assert.Equal(expected, vm.MatchesSearch(term));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void MatchesSearch_ReturnsTrue_WhenSearchTermIsNullOrWhiteSpace(string? term)
    {
        var vm = new ArtistViewModel { ArtistId = 1, Name = "Any Artist" };
        Assert.True(vm.MatchesSearch(term));
    }

    // ── Sorting helpers ───────────────────────────────────────────────────────

    [Fact]
    public void Artists_CanBeSortedByNameAscending()
    {
        var artists = new List<ArtistViewModel>
        {
            new() { Name = "Zeppelin" },
            new() { Name = "AC/DC" },
            new() { Name = "Beatles" }
        };

        var sorted = artists.OrderBy(a => a.Name).ToList();

        Assert.Equal("AC/DC",   sorted[0].Name);
        Assert.Equal("Beatles", sorted[1].Name);
        Assert.Equal("Zeppelin", sorted[2].Name);
    }

    [Fact]
    public void Artists_CanBeSortedByAlbumCountDescending()
    {
        var artists = new List<ArtistViewModel>
        {
            new() { Name = "A", AlbumCount = 1 },
            new() { Name = "B", AlbumCount = 5 },
            new() { Name = "C", AlbumCount = 3 }
        };

        var sorted = artists.OrderByDescending(a => a.AlbumCount).ToList();

        Assert.Equal(5, sorted[0].AlbumCount);
        Assert.Equal(3, sorted[1].AlbumCount);
        Assert.Equal(1, sorted[2].AlbumCount);
    }

    // ── Empty / edge cases ────────────────────────────────────────────────────

    [Fact]
    public void FilteredArtists_IsEmpty_WhenNoArtistsMatchSearch()
    {
        var artists = new List<ArtistViewModel>
        {
            new() { Name = "AC/DC" },
            new() { Name = "Beatles" }
        };

        var results = artists.Where(a => a.MatchesSearch("Metallica")).ToList();

        Assert.Empty(results);
    }

    [Fact]
    public void FilteredArtists_ReturnsAll_WhenSearchIsEmpty()
    {
        var artists = new List<ArtistViewModel>
        {
            new() { Name = "AC/DC" },
            new() { Name = "Beatles" }
        };

        var results = artists.Where(a => a.MatchesSearch(string.Empty)).ToList();

        Assert.Equal(2, results.Count);
    }
}
