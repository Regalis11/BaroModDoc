# Particles

<sup>Relevant files: [Shared:ParticlesFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/ParticlesFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single Particle

```xml
<Particle
  identifier="myParticle" />
```

### Example 2 - multiple Particles

```xml
<Particles>
  <Particle
    identifier="myParticle1" />
  <Particle
    identifier="myParticle2" />
</Particles>
```

### Example 3 - overriding existing Particles

```xml
<override>
  <Particle
    identifier="myParticle1" />
  <Particle
    identifier="myParticle2" />
</override>
```

