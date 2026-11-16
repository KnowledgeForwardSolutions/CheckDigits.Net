# S0086 - Cleanup and Prepare CheckDigits.Net for V4.0.0 Release

## User Story

As a maintainer of CheckDigits.Net, I want to clean up technical debt and modernize the project infrastructure so that the codebase is maintainable, follows current best practices, and is ready for production release as V4.0.0.

## Description

This story encompasses a comprehensive cleanup of the CheckDigits.Net project to remove obsolete code, modernize dependencies, implement solution-wide configuration files, and eliminate all compiler warnings.

## Acceptance Criteria

- [ ] All algorithms updated to use CharExtensions.cs and Int32Extensions.cs methods instead of obsolete ExtensionMethods.cs methods
- [ ] All classes and methods marked as obsolete in V3.1.0 are removed from the codebase
- [ ] Solution-wide .editorconfig file created and implemented across all projects
- [ ] Solution-wide Directory.Build.Props file created with common build configuration
- [ ] All compiler warnings resolved (build completes with zero warnings)
- [ ] xUnit dependency updated to v3 (latest stable)
- [ ] .NET 8 support removed from project files (out of long-term support)
- [ ] Build verification confirms successful compilation on all supported frameworks
- [ ] All tests pass on updated framework and xUnit versions

## Tasks

### Task 1: Update Obsolete Extension Methods

**Objective:** Replace all calls to deprecated methods in ExtensionMethods.cs with modern equivalents.

**Details:**
- Identify all algorithms and utilities currently using obsolete extension methods:
  - `.IsInvalidDigit()`
  - `.IsInvalidExtendedDecimalCheckDigit()`
  - `.ToDigitChar()`
  - `.ToExtendedDecimalCheckDigit()`
  - `.ToIntegerDigit()`
- Replace with corresponding methods from:
  - CharExtensions.cs (character mapping methods)
  - Int32Extensions.cs (integer conversion methods)
- Update affected ISO/IEC 7064 algorithms and other check digit implementations
- Search for all usages in production code and test code
- Verify all refactored code compiles and tests pass

**Files to Review:**
- CheckDigits.Net/Iso7064/*.cs
- CheckDigits.Net/Utility/*.cs
- CheckDigits.Net/*.cs (root algorithms)
- CheckDigits.Net.Tests.Unit/**/*.cs

**Acceptance Criteria:**
- [ ] Zero references to obsolete ExtensionMethods methods remain in production code
- [ ] All unit tests pass after refactoring
- [ ] No compilation errors or warnings introduced

---

### Task 2: Remove Obsolete Classes and Methods

**Objective:** Remove all code marked as Obsolete in V3.1.0 release.

**Details:**
- Remove obsolete classes:
  - ~~`CheckDigits.Net.Utility.ExtensionMethods` (entire class if no longer used)~~ CheckDigits.Net.Utility.ExtensionMethods was marked as obsolete in V4.0.0 release and will remain until the next major release.
- Remove obsolete algorithms:
  - `Modulus11Algorithm` (replaced by Modulus11Extended)
  - ~~`NhsAlgorithm` (replaced by Modulus11_27Decimal)~~ (while NhsAlgorithm is replaced by Modulus11_27Decimal, NhsAlgorithm includes length validation not included in Modulus11_27Decimal. Therefore NhsAlgorithm will be retained, but still marked as Obsolete to indicate to users that they should consider alternatives to NhsAlgorithm.)
- Remove all `[Obsolete]` attributes and associated methods
- Check ICAO 9303 algorithms for obsolete `LineSeparator` property and remove
- Update release notes and documentation to reflect removals

**Files to Review:**
- CheckDigits.Net/Utility/ExtensionMethods.cs
- CheckDigits.Net/Modulus/Modulus11Algorithm.cs
- CheckDigits.Net/Nhs/NhsAlgorithm.cs
- CheckDigits.Net/Icao9303/*.cs
- CheckDigits.Net/Algorithms.cs (public static instances)

**Acceptance Criteria:**
- [ ] All [Obsolete] attributes removed from codebase
- [ ] No warnings about obsolete members in build output
- [ ] Updated Algorithms.cs and static instances list (if applicable)
- [ ] All related tests either removed or migrated to replacement algorithms

---

### Task 3: Add Solution-Wide Configuration Files

**Objective:** Create .editorconfig and Directory.Build.Props files for consistent configuration across all projects.

**Details:**

**3a. Create .editorconfig file**
- Location: Solution root (`/CheckDigits.Net/.editorconfig`)
- Reference KfAccountNumbers project for template and best practices
- Include settings for:
  - C# code style (naming conventions, formatting)
  - File encoding (UTF-8)
  - Line endings (CRLF for Windows, configurable)
  - Tab/space preferences
  - Indentation standards
  - Brace positioning
  - Naming conventions for public/private members
- Apply to all C# files (*.cs, *.csproj)

**3b. Create Directory.Build.Props file**
- Location: Solution root (`/CheckDigits.Net/Directory.Build.Props`)
- Reference KfAccountNumbers project for examples
- Include common settings:
  - Target frameworks (remove Net8.0, keep Net10.0+)
  - Common NuGet package versions
  - Compiler warnings treatment
  - Code analysis settings
  - Documentation settings
  - Assembly information defaults (Company, Product, Copyright)

**3c. Create Directory.Build.targets (if needed)**
- Include any shared build tasks
- Example: code generation, documentation generation

**Files to Create:**
- `.editorconfig` (solution root)
- `Directory.Build.Props` (solution root)
- `Directory.Build.targets` (optional, solution root)

**Reference Projects:**
- Examine KfAccountNumbers/.editorconfig for style guidelines
- Examine KfAccountNumbers/Directory.Build.Props for property templates

**Acceptance Criteria:**
- [ ] .editorconfig file created at solution root
- [ ] Directory.Build.Props file created at solution root
- [ ] All project files (.csproj) inherit configuration without local overrides
- [ ] Documentation explains configuration for future maintainers

---

### Task 4: Resolve All Compiler Warnings

**Objective:** Clean build with zero warnings.

**Details:**
- Run full solution build targeting each supported framework
- Document and categorize all warnings found:
  - Obsolete member usage warnings (addressed in Task 2)
  - Style/naming violations (addressed by .editorconfig in Task 3)
  - Code analysis warnings (fix or suppress with justification)
  - Deprecation warnings (update framework version or dependencies)
- Fix warnings in priority order:
  1. Functional issues (null reference, uninitialized variables, etc.)
  2. Obsolete API usage
  3. Code analysis violations
  4. Style/naming issues
- Update .editorconfig or Directory.Build.Props to enforce new standards
- Document any suppressed warnings with `#pragma` and rationale

**Build Commands:**
```bash
dotnet clean
dotnet build --configuration Release --no-incremental
dotnet build --configuration Debug --no-incremental
```

**Acceptance Criteria:**
- [ ] Clean build with zero warnings on Release configuration
- [ ] Clean build with zero warnings on Debug configuration
- [ ] All frameworks build warning-free
- [ ] Build log saved as reference

---

### Task 5: Update xUnit to v3

**Objective:** Upgrade testing framework to xUnit v3 (latest stable).

**Details:**
- Review xUnit v3 release notes for breaking changes
- Update all project files (.csproj) referencing xUnit:
  - Update xunit version from current to v3.x.x (latest stable)
  - Update xunit.analyzers if used
  - Update xunit.runner.console if used
- Review test code for compatibility issues:
  - Verify no API breaking changes in test assertions
  - Check for new recommended practices or obsolete attributes
  - Update any test discovery patterns if needed
- Run full test suite to verify all tests pass
- Update CI/CD pipeline if applicable

**Files to Update:**
- CheckDigits.Net.Tests.Unit.csproj
- CheckDigits.Net.Benchmarks.csproj (if applicable)
- CI/CD configuration files

**Acceptance Criteria:**
- [ ] xUnit v3 installed in all test projects
- [ ] All tests pass with xUnit v3
- [ ] No deprecation warnings from xUnit
- [ ] Documentation updated to reflect xUnit v3 usage

---

### Task 6: Remove .NET 8 Support

**Objective:** Remove .NET 8 from the supported target frameworks (out of long-term support as of November 2024).

**Details:**
- Review all project files (.csproj) for TargetFrameworks specifications
- Identify current target frameworks:
  - Check current settings in Directory.Build.Props or individual .csproj files
  - Document which frameworks are currently targeted
- Remove net8.0 from TargetFrameworks lists
- Keep only long-term support versions:
  - Minimum: netstandard2.0 (if cross-platform support needed)
  - Current LTS: net10.0+ (verify latest LTS status)
  - Verify compatibility with V4.0.0 requirements
- Update .github/workflows if CI/CD tests against multiple frameworks
- Update README.md documentation with supported framework list
- Verify all unit tests pass against remaining frameworks

**Files to Update:**
- Directory.Build.Props (TargetFrameworks property)
- CheckDigits.Net/CheckDigits.Net.csproj
- CheckDigits.Net.Tests.Unit/CheckDigits.Net.Tests.Unit.csproj
- CheckDigits.Net.Benchmarks/CheckDigits.Net.Benchmarks.csproj
- README.md (supported frameworks section)
- CI/CD workflows (.github/workflows/*.yml)

**Acceptance Criteria:**
- [ ] .NET 8 removed from all TargetFrameworks specifications
- [ ] Build and test successful on all remaining target frameworks
- [ ] Documentation updated with new minimum/target framework versions
- [ ] CI/CD pipeline updated (if applicable)

---

### Task 7: Comprehensive Build Verification

**Objective:** Verify the entire project builds cleanly and all tests pass.

**Details:**
- Clean solution completely
- Restore all NuGet dependencies
- Build entire solution in both Debug and Release configurations
- Run full unit test suite
- Run benchmark suite (if applicable)
- Generate code coverage report (if configured)
- Verify no warnings in any configuration
- Test package creation (nuget pack) if applicable

**Build Checklist:**
```bash
dotnet clean -c Release
dotnet clean -c Debug
dotnet restore
dotnet build -c Debug --no-restore
dotnet build -c Release --no-restore
dotnet test
```

**Acceptance Criteria:**
- [ ] Solution builds successfully in Debug configuration
- [ ] Solution builds successfully in Release configuration
- [ ] All unit tests pass (100% pass rate)
- [ ] Zero warnings in build output
- [ ] All target frameworks compile successfully
- [ ] Code coverage meets project standards (if applicable)

---

## Definition of Done

- All acceptance criteria met
- All tasks completed and verified
- No compiler warnings remain
- Full test suite passes on all target frameworks
- Code reviewed and approved
- Documentation updated (README, release notes, etc.)
- Changes merged to main branch
- Ready for V4.0.0 release

## Estimated Effort

- **Task 1 (Obsolete Methods):** 2-3 days
- **Task 2 (Remove Obsolete):** 1-2 days
- **Task 3 (Config Files):** 1-2 days
- **Task 4 (Warnings):** 1-2 days
- **Task 5 (xUnit v3):** 1 day
- **Task 6 (.NET 8):** 1 day
- **Task 7 (Verification):** 1 day

**Total Estimated Effort:** 8-13 days (approximately 2 weeks)

## Dependencies

- Access to KfAccountNumbers project for .editorconfig and Directory.Build.Props examples
- xUnit v3 stable release available
- .NET 10.0 SDK and newer LTS frameworks installed locally

## Related User Stories

- Project creation and setup (related to build configuration)
- Algorithm implementations (refined by obsolete method removal)
- Test suite expansion and maintenance

## Notes

- Review KfAccountNumbers/.editorconfig and Directory.Build.Props early in Task 3 for reference implementations
- Consider creating a migration guide for any breaking changes
- Update CHANGELOG.md with all breaking changes in V4.0.0
- Verify backward compatibility impact of removing obsolete members
- Test on multiple machine/OS configurations if possible

## Tags

- `cleanup`
- `technical-debt`
- `v4.0.0-release`
- `code-quality`
- `build-configuration`
- `framework-upgrade`

