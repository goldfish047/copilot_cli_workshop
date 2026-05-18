using Chinook.Web.Models;
using Chinook.Web.ViewModels;

namespace Chinook.Web.Tests;

/// <summary>
/// Tests for AlbumViewModel construction, artist-name join, track count accuracy,
/// search filtering, empty results, and null/edge-case data validation.
/// </summary>
public class AlbumViewModelTests
{
    // ── Construction ──────────────────────────────────────────────────────────

    [Fact]
    public void FromAlbum_MapsIdAndTitle()
    {
        var album = BuildAlbum(albumId: 10, title: "Back in Black", artistName: "AC/DC");
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal(10, vm.AlbumId);
        Assert.Equal("Back in Black", vm.Title);
    }

    [Fact]
    public void FromAlbum_JoinsArtistName()
    {
        var album = BuildAlbum(albumId: 10, title: "Back in Black", artistName: "AC/DC");
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal("AC/DC", vm.ArtistName);
    }

    [Fact]
    public void FromAlbum_ArtistName_IsEmptyString_WhenArtistIsNull()
    {
        var album = new Album { AlbumId = 99, Title = "Unknown Album", ArtistId = 0 };
        // Artist navigation property left null
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal(string.Empty, vm.ArtistName);
    }

    [Fact]
    public void FromAlbum_ArtistName_IsEmptyString_WhenArtistNameIsNull()
    {
        var album = new Album
        {
            AlbumId = 99,
            Title = "Unnamed",
            ArtistId = 1,
            Artist = new Artist { ArtistId = 1, Name = null }
        };
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal(string.Empty, vm.ArtistName);
    }

    [Fact]
    public void FromAlbum_CountsTracksCorrectly()
    {
        var album = BuildAlbum(albumId: 1, title: "Abbey Road", artistName: "Beatles",
            trackCount: 3);
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal(3, vm.TrackCount);
    }

    [Fact]
    public void FromAlbum_TrackCount_IsZero_WhenAlbumHasNoTracks()
    {
        var album = BuildAlbum(albumId: 1, title: "Empty Album", artistName: "Artist");
        var vm = AlbumViewModel.FromAlbum(album);

        Assert.Equal(0, vm.TrackCount);
    }

    // ── Search filtering ──────────────────────────────────────────────────────

    [Theory]
    [InlineData("back", true)]          // title match (lower-case)
    [InlineData("Back in Black", true)] // exact title
    [InlineData("AC/DC", true)]         // artist name match
    [InlineData("ac/dc", true)]         // artist name case-insensitive
    [InlineData("Beatles", false)]      // no match on artist
    [InlineData("Highway", false)]      // no match on title
    public void MatchesSearch_ReturnsExpected(string term, bool expected)
    {
        var vm = new AlbumViewModel
        {
            AlbumId = 1,
            Title = "Back in Black",
            ArtistName = "AC/DC"
        };

        Assert.Equal(expected, vm.MatchesSearch(term));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void MatchesSearch_ReturnsTrue_WhenSearchTermIsNullOrWhiteSpace(string? term)
    {
        var vm = new AlbumViewModel { Title = "Any Album", ArtistName = "Any Artist" };
        Assert.True(vm.MatchesSearch(term));
    }

    // ── Sorting helpers ───────────────────────────────────────────────────────

    [Fact]
    public void Albums_CanBeSortedByTitleAscending()
    {
        var albums = new List<AlbumViewModel>
        {
            new() { Title = "Zephyr Song" },
            new() { Title = "Abbey Road" },
            new() { Title = "Master of Puppets" }
        };

        var sorted = albums.OrderBy(a => a.Title).ToList();

        Assert.Equal("Abbey Road",        sorted[0].Title);
        Assert.Equal("Master of Puppets", sorted[1].Title);
        Assert.Equal("Zephyr Song",       sorted[2].Title);
    }

    [Fact]
    public void Albums_CanBeSortedByArtistNameAscending()
    {
        var albums = new List<AlbumViewModel>
        {
            new() { Title = "T1", ArtistName = "Zeppelin" },
            new() { Title = "T2", ArtistName = "AC/DC" },
            new() { Title = "T3", ArtistName = "Metallica" }
        };

        var sorted = albums.OrderBy(a => a.ArtistName).ToList();

        Assert.Equal("AC/DC",    sorted[0].ArtistName);
        Assert.Equal("Metallica", sorted[1].ArtistName);
        Assert.Equal("Zeppelin", sorted[2].ArtistName);
    }

    // ── Empty / edge cases ────────────────────────────────────────────────────

    [Fact]
    public void FilteredAlbums_IsEmpty_WhenNoAlbumsMatchSearch()
    {
        var albums = new List<AlbumViewModel>
        {
            new() { Title = "Back in Black", ArtistName = "AC/DC" }
        };

        var results = albums.Where(a => a.MatchesSearch("Beatles")).ToList();

        Assert.Empty(results);
    }

    [Fact]
    public void FilteredAlbums_ReturnsAll_WhenSearchIsEmpty()
    {
        var albums = new List<AlbumViewModel>
        {
            new() { Title = "T1", ArtistName = "A1" },
            new() { Title = "T2", ArtistName = "A2" }
        };

        var results = albums.Where(a => a.MatchesSearch("")).ToList();

        Assert.Equal(2, results.Count);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static Album BuildAlbum(int albumId, string title, string artistName,
        int trackCount = 0)
    {
        var artist = new Artist { ArtistId = albumId, Name = artistName };
        var album = new Album { AlbumId = albumId, Title = title, ArtistId = artist.ArtistId, Artist = artist };
        for (var i = 1; i <= trackCount; i++)
        {
            album.Tracks.Add(new Track
            {
                TrackId = i,
                Name = $"Track {i}",
                AlbumId = albumId,
                MediaTypeId = 1,
                Milliseconds = 200_000,
                UnitPrice = 0.99
            });
        }
        return album;
    }
}
