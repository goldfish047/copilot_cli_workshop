using Chinook.Web.Models;
using Chinook.Web.ViewModels;

namespace Chinook.Web.Tests;

/// <summary>
/// Tests for TrackViewModel construction, album/artist name joins,
/// duration formatting, price display, search filtering, and edge cases.
/// </summary>
public class TrackViewModelTests
{
    // ── Construction ──────────────────────────────────────────────────────────

    [Fact]
    public void FromTrack_MapsIdAndName()
    {
        var track = BuildTrack(trackId: 1, name: "Highway to Hell", albumTitle: "Highway to Hell",
            artistName: "AC/DC", milliseconds: 208_000, price: 0.99);

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal(1, vm.TrackId);
        Assert.Equal("Highway to Hell", vm.Name);
    }

    [Fact]
    public void FromTrack_JoinsAlbumTitle()
    {
        var track = BuildTrack(trackId: 1, name: "Stairway to Heaven",
            albumTitle: "Led Zeppelin IV", artistName: "Led Zeppelin",
            milliseconds: 482_000, price: 0.99);

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal("Led Zeppelin IV", vm.AlbumTitle);
    }

    [Fact]
    public void FromTrack_JoinsArtistName()
    {
        var track = BuildTrack(trackId: 1, name: "Stairway to Heaven",
            albumTitle: "Led Zeppelin IV", artistName: "Led Zeppelin",
            milliseconds: 482_000, price: 0.99);

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal("Led Zeppelin", vm.ArtistName);
    }

    [Fact]
    public void FromTrack_AlbumTitle_IsEmptyString_WhenAlbumIsNull()
    {
        var track = new Track
        {
            TrackId = 99, Name = "Orphan Track", MediaTypeId = 1,
            Milliseconds = 100_000, UnitPrice = 0.99
        };

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal(string.Empty, vm.AlbumTitle);
    }

    [Fact]
    public void FromTrack_ArtistName_IsEmptyString_WhenAlbumIsNull()
    {
        var track = new Track
        {
            TrackId = 99, Name = "Orphan Track", MediaTypeId = 1,
            Milliseconds = 100_000, UnitPrice = 0.99
        };

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal(string.Empty, vm.ArtistName);
    }

    [Fact]
    public void FromTrack_ArtistName_IsEmptyString_WhenArtistIsNull()
    {
        var album = new Album { AlbumId = 1, Title = "No Artist Album", ArtistId = 0 };
        var track = new Track
        {
            TrackId = 1, Name = "Track", AlbumId = 1, Album = album,
            MediaTypeId = 1, Milliseconds = 100_000, UnitPrice = 0.99
        };

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal(string.Empty, vm.ArtistName);
    }

    // ── Duration formatting ───────────────────────────────────────────────────

    [Theory]
    [InlineData(0,       "0:00")]   // zero
    [InlineData(1_000,   "0:01")]   // 1 second
    [InlineData(59_000,  "0:59")]   // 59 seconds
    [InlineData(60_000,  "1:00")]   // exactly 1 minute
    [InlineData(61_000,  "1:01")]   // 1 min 1 sec
    [InlineData(208_000, "3:28")]   // ~3.5 minutes
    [InlineData(482_000, "8:02")]   // ~8 minutes
    [InlineData(3_600_000, "60:00")] // 1 hour
    public void FormatDuration_ReturnsExpectedString(int milliseconds, string expected)
    {
        Assert.Equal(expected, TrackViewModel.FormatDuration(milliseconds));
    }

    [Fact]
    public void FromTrack_Duration_IsFormatted()
    {
        var track = BuildTrack(trackId: 1, name: "Test", albumTitle: "A", artistName: "B",
            milliseconds: 208_000, price: 0.99);

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal("3:28", vm.Duration);
    }

    // ── Price display ─────────────────────────────────────────────────────────

    [Theory]
    [InlineData(0.99,  "$0.99")]
    [InlineData(1.29,  "$1.29")]
    [InlineData(9.99,  "$9.99")]
    [InlineData(0.00,  "$0.00")]
    [InlineData(10.00, "$10.00")]
    public void PriceDisplay_FormatsAsExpected(double price, string expected)
    {
        var vm = new TrackViewModel { UnitPrice = price };
        Assert.Equal(expected, vm.PriceDisplay);
    }

    [Fact]
    public void FromTrack_UnitPrice_IsPreserved()
    {
        var track = BuildTrack(trackId: 1, name: "Test", albumTitle: "A", artistName: "B",
            milliseconds: 200_000, price: 1.29);

        var vm = TrackViewModel.FromTrack(track);

        Assert.Equal(1.29, vm.UnitPrice);
    }

    // ── Search filtering ──────────────────────────────────────────────────────

    [Theory]
    [InlineData("Highway",       true)]   // track name match
    [InlineData("highway",       true)]   // case-insensitive name
    [InlineData("Black",         true)]   // album title match
    [InlineData("black",         true)]   // album case-insensitive
    [InlineData("AC/DC",         true)]   // artist name match
    [InlineData("ac/dc",         true)]   // artist case-insensitive
    [InlineData("Metallica",     false)]  // no match
    [InlineData("Stairway",      false)]  // no match on different track
    public void MatchesSearch_ReturnsExpected(string term, bool expected)
    {
        var vm = new TrackViewModel
        {
            Name = "Highway to Hell",
            AlbumTitle = "Back in Black",
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
        var vm = new TrackViewModel { Name = "Any", AlbumTitle = "Any", ArtistName = "Any" };
        Assert.True(vm.MatchesSearch(term));
    }

    // ── Sorting helpers ───────────────────────────────────────────────────────

    [Fact]
    public void Tracks_CanBeSortedByNameAscending()
    {
        var tracks = new List<TrackViewModel>
        {
            new() { Name = "Zoo Station" },
            new() { Name = "All Apologies" },
            new() { Name = "Master of Puppets" }
        };

        var sorted = tracks.OrderBy(t => t.Name).ToList();

        Assert.Equal("All Apologies",     sorted[0].Name);
        Assert.Equal("Master of Puppets", sorted[1].Name);
        Assert.Equal("Zoo Station",       sorted[2].Name);
    }

    [Fact]
    public void Tracks_CanBeSortedByPriceDescending()
    {
        var tracks = new List<TrackViewModel>
        {
            new() { Name = "T1", UnitPrice = 0.99 },
            new() { Name = "T2", UnitPrice = 1.29 },
            new() { Name = "T3", UnitPrice = 0.00 }
        };

        var sorted = tracks.OrderByDescending(t => t.UnitPrice).ToList();

        Assert.Equal(1.29, sorted[0].UnitPrice);
        Assert.Equal(0.99, sorted[1].UnitPrice);
        Assert.Equal(0.00, sorted[2].UnitPrice);
    }

    // ── Empty / edge cases ────────────────────────────────────────────────────

    [Fact]
    public void FilteredTracks_IsEmpty_WhenNoTracksMatchSearch()
    {
        var tracks = new List<TrackViewModel>
        {
            new() { Name = "Highway to Hell", AlbumTitle = "Highway to Hell", ArtistName = "AC/DC" }
        };

        var results = tracks.Where(t => t.MatchesSearch("Nirvana")).ToList();

        Assert.Empty(results);
    }

    [Fact]
    public void FilteredTracks_ReturnsAll_WhenSearchIsEmpty()
    {
        var tracks = new List<TrackViewModel>
        {
            new() { Name = "T1", AlbumTitle = "A1", ArtistName = "B1" },
            new() { Name = "T2", AlbumTitle = "A2", ArtistName = "B2" }
        };

        var results = tracks.Where(t => t.MatchesSearch("")).ToList();

        Assert.Equal(2, results.Count);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private static Track BuildTrack(int trackId, string name, string albumTitle,
        string artistName, int milliseconds, double price)
    {
        var artist = new Artist { ArtistId = 1, Name = artistName };
        var album  = new Album  { AlbumId = 1, Title = albumTitle, ArtistId = 1, Artist = artist };
        return new Track
        {
            TrackId      = trackId,
            Name         = name,
            AlbumId      = album.AlbumId,
            Album        = album,
            MediaTypeId  = 1,
            Milliseconds = milliseconds,
            UnitPrice    = price
        };
    }
}
