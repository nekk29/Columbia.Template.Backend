# Bash Script

### Usage:
```bash
./generate-solution.sh <Namespace> <ClientCode> <ClientDescription> [OutputDir]
```

### Example:
```bash
./generate-solution.sh "Eleven.Security" "security" "Security Apis"
```

# .Net Executable

### Usage:
```bash
./GenerateSolution <Namespace> <ClientCode> <ClientDescription> [OutputDir]
```

### Example:
```bash
./GenerateSolution "Eleven.Security" "security" "Security Apis"
```

## Parameters:

| Parameter | Description  |
| ------- | --- |
| **Namespace** | The root namespace for all projects. Example: Eleven.Security |
| **ClientCode** | Short lowercase code used as the OpenIddict/ASP.NET Identity application/client identifier (no spaces). Example: security |
| **ClientDescription** | Human-readable name used as the OpenIddict application display name and in the Application table. Example: "Security Apis" |
| **OutputDir** | (Optional) Directory where the solution will be created. Defaults to ./\<Namespace\> in the current working directory. |
