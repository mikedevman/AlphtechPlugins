using Microsoft.EntityFrameworkCore;
using AlphtechPlugins.Models;
using System.Text.Json;

namespace AlphtechPlugins.Data;

public class AlphtechDbContext : DbContext
{
    public AlphtechDbContext(DbContextOptions<AlphtechDbContext> options)
        : base(options)
    {
    }

    public DbSet<PluginProduct> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product entity configuration
        modelBuilder.Entity<PluginProduct>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price).HasPrecision(18, 2);

            // Store Features list as JSON string in SQLite
            entity.Property(e => e.Features)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => string.IsNullOrEmpty(v) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                );
        });

        // Order entity configuration
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.Total).HasPrecision(18, 2);

            entity.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderItem entity configuration
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });
    }

    public static void SeedData(AlphtechDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Products.Any())
        {
            var seedProducts = new List<PluginProduct>
            {
                new PluginProduct
                {
                    Id = "tubepre-2",
                    Name = "Alphtech TubePre 2.0",
                    Tagline = "Analog 12AX7 Dual-Triode Valve Preamplifier",
                    Category = "Guitar & Bass",
                    Price = 0.00m,
                    Description = "Studio-grade 12AX7 vacuum tube preamplifier emulation with authentic valve saturation, harmonic overtone generation, and analog warmth. Modeled from a vintage Marshall JCM800 preamp circuit with component-level accuracy.",
                    ImageUrl = "/images/tubepre.svg",
                    IconClass = "bi-lightning-charge-fill",
                    Rating = 4.9,
                    Downloads = 12450,
                    Features = new() { "12AX7 dual-triode valve modeling", "Drive, Bass, Mid, Treble, Presence controls", "Cabinet IR loader with 8 built-in impulses", "Analog noise floor simulation", "Oversampling up to 16x" },
                    SampleRate = "Up to 192kHz",
                    BitDepth = "64-bit floating point",
                    Latency = "Zero latency"
                },
                new PluginProduct
                {
                    Id = "british-plexi",
                    Name = "Alphtech British Plexi 100W",
                    Tagline = "Vintage British Full-Stack Amplifier",
                    Category = "Guitar & Bass",
                    Price = 0.00m,
                    Description = "Faithful recreation of the legendary 1959 Plexi 100W Super Lead amplifier. From crystalline cleans to roaring classic rock crunch, this plugin captures the iconic British tone used on countless hit records.",
                    ImageUrl = "/images/plexi.svg",
                    IconClass = "bi-speaker-fill",
                    Rating = 4.8,
                    Downloads = 9870,
                    Features = new() { "Full preamp and power amp modeling", "EL34 power tube saturation", "Bright/Normal channel with jumper mode", "Reactive speaker cab simulation", "Built-in noise gate" },
                    SampleRate = "Up to 192kHz",
                    BitDepth = "64-bit floating point",
                    Latency = "Zero latency"
                },
                new PluginProduct
                {
                    Id = "master-opto",
                    Name = "Alphtech MasterOpto",
                    Tagline = "Optical Studio Compressor & Limiter",
                    Category = "Dynamics & Mixing",
                    Price = 0.00m,
                    Description = "Transparent optical compression inspired by the legendary LA-2A. Features program-dependent attack and release with silky smooth gain reduction perfect for vocals, bass, and mix bus duties.",
                    ImageUrl = "/images/opto.svg",
                    IconClass = "bi-soundwave",
                    Rating = 4.7,
                    Downloads = 8340,
                    Features = new() { "Optical gain reduction circuit", "Program-dependent attack/release", "Peak Reduction & Gain knobs", "Compress/Limit mode switch", "Sidechain HPF filter" },
                    SampleRate = "Up to 192kHz",
                    BitDepth = "64-bit floating point",
                    Latency = "Zero latency"
                },
                new PluginProduct
                {
                    Id = "spatial-verb",
                    Name = "Alphtech SpatialVerb 3D",
                    Tagline = "Convolution Reverb Engine",
                    Category = "Spatial & Effects",
                    Price = 0.00m,
                    Description = "Professional convolution reverb with 50+ impulse responses captured from legendary studios, concert halls, and plate reverbs worldwide. Includes algorithmic tail generation for infinite sustain.",
                    ImageUrl = "/images/reverb.svg",
                    IconClass = "bi-badge-3d-fill",
                    Rating = 4.6,
                    Downloads = 7210,
                    Features = new() { "50+ studio & hall impulse responses", "True stereo convolution engine", "Pre-delay, decay, and damping controls", "Algorithmic tail extension", "Dry/Wet mix with lock" },
                    SampleRate = "Up to 96kHz",
                    BitDepth = "64-bit floating point",
                    Latency = "256 samples"
                },
                new PluginProduct
                {
                    Id = "tape-sat",
                    Name = "Alphtech TapeSat 1978",
                    Tagline = "Vintage Reel-to-Reel Tape Saturator",
                    Category = "Dynamics & Mixing",
                    Price = 0.00m,
                    Description = "Analog tape saturation and compression modeled from a 1978 Studer A800 MkIII 2-inch reel-to-reel machine. Adds warmth, harmonic density, and gentle compression to any source.",
                    ImageUrl = "/images/tape.svg",
                    IconClass = "bi-vinyl-fill",
                    Rating = 4.8,
                    Downloads = 6580,
                    Features = new() { "Studer A800 tape path modeling", "Input/Output gain staging", "Tape speed: 15/30 IPS", "Bias and saturation controls", "Wow & flutter simulation" },
                    SampleRate = "Up to 192kHz",
                    BitDepth = "64-bit floating point",
                    Latency = "Zero latency"
                },
                new PluginProduct
                {
                    Id = "subsonic-synth",
                    Name = "Alphtech SubSonic",
                    Tagline = "Dual-Oscillator Bass Synthesizer",
                    Category = "Virtual Instruments",
                    Price = 0.00m,
                    Description = "Heavyweight bass synthesizer with dual analog-modeled oscillators, sub-harmonic generator, and multi-mode filter designed for modern electronic music production, hip-hop, and sound design.",
                    ImageUrl = "/images/synth.svg",
                    IconClass = "bi-tsunami",
                    Rating = 4.5,
                    Downloads = 5920,
                    Features = new() { "Dual oscillators (Saw, Square, Sine, Triangle)", "Sub-harmonic oscillator", "Multi-mode filter (LP, HP, BP, Notch)", "2x ADSR envelopes", "Built-in distortion & chorus" },
                    SampleRate = "Up to 96kHz",
                    BitDepth = "32-bit floating point",
                    Latency = "64 samples"
                }
            };

            context.Products.AddRange(seedProducts);
            context.SaveChanges();
        }
    }
}
