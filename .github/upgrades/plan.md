# .NET 10.0 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [RazorDemo.csproj](#razordemocsproj)
  - [DrewBrasher.OrchardCore.Blazor.csproj](#drewbrasherorchardcoreblazorcsproj)
  - [DrewBrasher.OrchardCore.ContentWarning.csproj](#drewbrasherorchardcorecontentwarningcsproj)
  - [DrewBrasher.OrchardCore.ExternalData.csproj](#drewbrasherorchardcoreexternaldatacsproj)
  - [OCRazorModuleDemo.csproj](#ocrazormoduledemocsproj)
  - [DrewBrasher.OrchardCore.CmsWeb.csproj](#drewbrasherorchardcorecmswebcsproj)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Overview

This plan outlines the upgrade of the DrewBrasher.OrchardCore solution from **.NET 8.0** to **.NET 10.0 (Long Term Support)**. The solution consists of 6 Orchard Core modules and applications targeting a modern web CMS architecture.

### Scope

**Projects Affected:** 6 projects (all require framework upgrade)
- 4 Orchard Core Module libraries (ContentWarning, ExternalData, Blazor, OCRazorModuleDemo)
- 1 Razor component library (RazorDemo)
- 1 ASP.NET Core web application (CmsWeb)

**Current State:** All projects target net8.0  
**Target State:** All projects target net10.0

### Key Metrics

| Metric | Value | Assessment |
|--------|-------|------------|
| Total Projects | 6 | All require upgrade |
| Total LOC | 757 | Small codebase |
| Dependency Depth | 2 levels | Simple hierarchy |
| Package Updates | 1 of 15 | 93.3% compatible |
| API Issues | 3 behavioral changes | Low runtime impact |
| Security Vulnerabilities | 0 | ? None found |
| Estimated LOC Impact | 3+ lines | 0.4% of codebase |

### Selected Strategy

**All-At-Once Strategy** - All projects upgraded simultaneously in single coordinated operation.

**Rationale:**
- Small solution (6 projects, 757 LOC)
- All projects currently on .NET 8.0 (homogeneous)
- Simple dependency structure (2-level depth, no cycles)
- Excellent package compatibility (93.3%)
- Low complexity (all projects rated ?? Low difficulty)
- Minimal breaking changes (3 behavioral changes only)
- No security vulnerabilities requiring immediate attention

### Critical Issues

**None.** This is a low-risk upgrade with:
- No security vulnerabilities
- No binary incompatible API changes
- Only 3 behavioral changes requiring runtime validation
- 1 package update (Microsoft.AspNetCore.Components.Web 8.0.14 ? 10.0.1)

### Iteration Strategy

This plan will be completed in **4-5 iterations**:
1. ? **Phase 1: Discovery & Classification** (Complete)
2. **Phase 2: Foundation** - Dependency analysis, migration strategy, project stubs
3. **Phase 3: Detailed Specification** - Single batch (all projects together, simple solution)
4. **Final: Success Criteria & Source Control**

### Recommended Approach

Execute the upgrade as a **single atomic operation**:
1. Update all 6 project files to net10.0
2. Update Microsoft.AspNetCore.Components.Web package in RazorDemo
3. Restore dependencies and build entire solution
4. Fix any compilation errors (expected to be minimal)
5. Validate behavioral changes through runtime testing
6. Commit as single unified changeset

**Estimated Complexity:** Low  
**Expected Timeline:** Single development session (coordination over atomicity preferred)

---

## Migration Strategy

### Strategic Approach: All-At-Once

This upgrade will follow the **All-At-Once Strategy**, where all 6 projects are upgraded to .NET 10.0 simultaneously in a single coordinated operation.

#### Why All-At-Once?

This solution exhibits all the ideal characteristics for atomic upgrade:

? **Small Scale**: 6 projects totaling 757 LOC  
? **Homogeneous State**: All projects currently on .NET 8.0  
? **Simple Dependencies**: 2-level hierarchy, no cycles  
? **High Compatibility**: 93.3% of packages already compatible  
? **Low Risk**: All projects rated ?? Low difficulty  
? **Minimal Changes**: Only 1 package update required  
? **No Security Issues**: No vulnerabilities forcing staged approach  

#### Advantages for This Solution

- **Speed**: Complete upgrade in single development session
- **Simplicity**: No multi-targeting complexity
- **Consistency**: All projects move to .NET 10.0 simultaneously
- **Clean Testing**: Test once against final state, not intermediate states
- **Single Commit**: Unified changeset for easier rollback if needed

#### Risks and Mitigations

| Risk | Likelihood | Mitigation |
|------|------------|------------|
| Multiple projects fail to build | Low | All projects rated Low difficulty; assess build errors as group |
| Behavioral changes cause runtime issues | Low | Only 3 behavioral changes; targeted runtime testing after build succeeds |
| Package compatibility issues | Very Low | 93.3% already compatible; only 1 package needs update |
| Breaking changes in OrchardCore modules | Low | OrchardCore packages marked compatible; framework isolation likely |

### Execution Phases

The All-At-Once upgrade follows these sequential phases:

#### Phase 0: Preparation
- Verify .NET 10.0 SDK installed on development machine
- Confirm starting from clean state on `upgrade-to-NET10` branch
- Validate no pending uncommitted changes

#### Phase 1: Atomic Framework & Package Update
**Single coordinated operation - all changes applied together:**

1. **Update all project TargetFramework properties** to `net10.0`:
   - RazorDemo.csproj
   - DrewBrasher.OrchardCore.Blazor.csproj
   - DrewBrasher.OrchardCore.ContentWarning.csproj
   - DrewBrasher.OrchardCore.ExternalData.csproj
   - OCRazorModuleDemo.csproj
   - DrewBrasher.OrchardCore.CmsWeb.csproj

2. **Update package reference**:
   - RazorDemo: Microsoft.AspNetCore.Components.Web 8.0.14 ? 10.0.1

3. **Restore dependencies** across solution:
   ```bash
   dotnet restore DrewBrasher.OrchardCore.sln
   ```

4. **Build solution** to identify compilation errors:
   ```bash
   dotnet build DrewBrasher.OrchardCore.sln
   ```

5. **Fix compilation errors** (if any):
   - Review compiler diagnostics
   - Apply fixes based on Breaking Changes Catalog (§Breaking Changes Catalog)
   - Expected: minimal to no errors based on assessment

6. **Rebuild and verify**:
   ```bash
   dotnet build DrewBrasher.OrchardCore.sln --no-incremental
   ```

**Success Criteria:** Solution builds with 0 errors

#### Phase 2: Runtime Validation

After successful build, validate behavioral changes:

1. **Review behavioral change locations**:
   - DrewBrasher.OrchardCore.CmsWeb (1 behavioral change)
   - DrewBrasher.OrchardCore.ExternalData (2 behavioral changes)

2. **Execute runtime tests**:
   - Start CmsWeb application
   - Verify exception handler behavior (UseExceptionHandler API change)
   - Test HttpClient usage (AddHttpClient behavioral change)
   - Test external data module HTTP operations (HttpContent behavioral change)

3. **Validate OrchardCore module integration**:
   - Verify all modules load correctly
   - Test content management features (ContentWarning, ExternalData)
   - Verify Blazor component rendering (OCRazorModuleDemo, RazorDemo)

**Success Criteria:** All behavioral validations pass without runtime exceptions

#### Phase 3: Final Verification & Commit

1. **Comprehensive solution build**:
   ```bash
   dotnet build DrewBrasher.OrchardCore.sln --configuration Release
   ```

2. **Visual inspection**:
   - Review all project files for correct TargetFramework
   - Verify package version updates applied
   - Confirm no unexpected changes

3. **Commit changes**:
   ```bash
   git add .
   git commit -m "Upgrade solution from .NET 8.0 to .NET 10.0 (LTS)
   
   - Updated all 6 projects to net10.0
   - Upgraded Microsoft.AspNetCore.Components.Web 8.0.14 ? 10.0.1 in RazorDemo
   - Validated 3 behavioral API changes
   - Solution builds successfully with 0 errors"
   ```

### Dependency-Based Ordering Principles

While this is an All-At-Once upgrade (all changes applied simultaneously), validation and troubleshooting should respect dependency order:

1. **If build fails**: Start diagnosis at leaf projects (no dependencies), then move up the dependency chain
2. **If runtime issues occur**: Test leaf modules independently, then intermediate module (OCRazorModuleDemo), finally root application (CmsWeb)
3. **For behavioral changes**: Test in the project where the API is used (see Breaking Changes Catalog)

### Parallel vs Sequential Execution

**File Updates**: Can be applied in **any order** or **in parallel** (all are independent file modifications)

**Build Validation**: Should follow **dependency order** (leaf ? intermediate ? root) for clearest error diagnostics, though `dotnet build` on solution will handle this automatically

**Runtime Testing**: Should be **sequential** following critical path to isolate issues

### Rollback Strategy

If the upgrade encounters blocking issues:

1. **Before Commit**: Simple `git reset --hard` to restore pre-upgrade state
2. **After Commit**: 
   ```bash
   git revert <commit-hash>
   ```
   Or create bugfix branch from commit before upgrade

Given the low-risk nature of this upgrade, rollback is unlikely to be necessary.

---

## Detailed Dependency Analysis

### Dependency Structure Overview

The solution has a clean 2-level dependency hierarchy with no circular dependencies, making it ideal for All-At-Once upgrade strategy.

```
Level 0 (Leaf Nodes - No Dependencies):
??? RazorDemo.csproj
??? DrewBrasher.OrchardCore.Blazor.csproj
??? DrewBrasher.OrchardCore.ContentWarning.csproj
??? DrewBrasher.OrchardCore.ExternalData.csproj

Level 1 (Intermediate):
??? OCRazorModuleDemo.csproj
    ??? depends on ? RazorDemo.csproj
    ??? depends on ? DrewBrasher.OrchardCore.Blazor.csproj

Level 2 (Root Application):
??? DrewBrasher.OrchardCore.CmsWeb.csproj (ASP.NET Core Web App)
    ??? depends on ? OCRazorModuleDemo.csproj
    ??? depends on ? DrewBrasher.OrchardCore.ContentWarning.csproj
    ??? depends on ? DrewBrasher.OrchardCore.ExternalData.csproj
```

### Dependency Matrix

| Project | Dependencies | Dependants | Type |
|---------|--------------|------------|------|
| **RazorDemo** | 0 | 1 (OCRazorModuleDemo) | Leaf - Razor Library |
| **DrewBrasher.OrchardCore.Blazor** | 0 | 1 (OCRazorModuleDemo) | Leaf - Blazor Module |
| **DrewBrasher.OrchardCore.ContentWarning** | 0 | 1 (CmsWeb) | Leaf - OC Module |
| **DrewBrasher.OrchardCore.ExternalData** | 0 | 1 (CmsWeb) | Leaf - OC Module |
| **OCRazorModuleDemo** | 2 | 1 (CmsWeb) | Intermediate - OC Module |
| **DrewBrasher.OrchardCore.CmsWeb** | 3 (+ transitive) | 0 | Root - Web Application |

### Project Groupings for All-At-Once Migration

Since this is an All-At-Once strategy, all projects are upgraded simultaneously in a single operation. However, understanding the logical groupings helps with validation:

**Group A: Foundation Libraries (4 projects)**
- RazorDemo
- DrewBrasher.OrchardCore.Blazor
- DrewBrasher.OrchardCore.ContentWarning
- DrewBrasher.OrchardCore.ExternalData

These have no project dependencies and can be validated independently after upgrade.

**Group B: Aggregation Module (1 project)**
- OCRazorModuleDemo

Depends on Group A projects. Will validate integration with updated dependencies.

**Group C: Application Entry Point (1 project)**
- DrewBrasher.OrchardCore.CmsWeb

The web application that consumes all modules. Final validation point for the entire solution.

### Critical Path

The critical path for build success follows the dependency chain:

```
RazorDemo ? OCRazorModuleDemo ? CmsWeb (via OCRazorModuleDemo)
DrewBrasher.OrchardCore.Blazor ? OCRazorModuleDemo ? CmsWeb (via OCRazorModuleDemo)
DrewBrasher.OrchardCore.ContentWarning ? CmsWeb (direct)
DrewBrasher.OrchardCore.ExternalData ? CmsWeb (direct)
```

All paths converge at **DrewBrasher.OrchardCore.CmsWeb**, which is the ultimate validation point.

### Circular Dependency Analysis

**Result:** ? **No circular dependencies detected**

The dependency graph is a clean directed acyclic graph (DAG), eliminating concerns about:
- Multi-targeting requirements
- Coordinated breaking changes
- Complex upgrade sequencing

### All-At-Once Execution Order

For the atomic upgrade operation, project files and packages will be updated in this order (respecting build dependencies):

1. **Phase 1: Leaf Projects** (can be updated in any order or parallel)
   - RazorDemo
   - DrewBrasher.OrchardCore.Blazor
   - DrewBrasher.OrchardCore.ContentWarning
   - DrewBrasher.OrchardCore.ExternalData

2. **Phase 2: Intermediate Project**
   - OCRazorModuleDemo

3. **Phase 3: Root Application**
   - DrewBrasher.OrchardCore.CmsWeb

**Important:** While updates can be applied in any order, the **build and validation** must respect this dependency order to surface compilation errors correctly.

---

## Project-by-Project Plans

This section provides detailed migration specifications for each project in dependency order (leaf nodes first, root application last).

### RazorDemo.csproj

**Path:** `Demo\RazorDemo\RazorDemo.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** ClassLibrary (Razor SDK)
- **Dependencies:** 0 project references
- **Dependants:** 1 (OCRazorModuleDemo)
- **LOC:** 36
- **Files:** 5
- **Risk Level:** ?? Low
- **Package Count:** 1
  - Microsoft.AspNetCore.Components.Web 8.0.14

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 1
  - Microsoft.AspNetCore.Components.Web 10.0.1

#### Migration Steps

1. **Prerequisites**
   - No project dependencies to migrate first
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `Demo\RazorDemo\RazorDemo.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Target Version | Reason |
   |---------|----------------|----------------|--------|
   | Microsoft.AspNetCore.Components.Web | 8.0.14 | 10.0.1 | Framework alignment; recommended upgrade for .NET 10.0 |

   Update in `Demo\RazorDemo\RazorDemo.csproj`:
   ```xml
   <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="10.0.1" />
   ```

4. **Expected Breaking Changes**
   
   **None.** Assessment shows:
   - 0 binary incompatible APIs
   - 0 source incompatible APIs  
   - 0 behavioral changes in this project
   - 84 APIs analyzed, all compatible

5. **Code Modifications**
   
   **None expected.** This is a simple Razor component library with full API compatibility.

6. **Testing Strategy**
   
   - **Build Test:** Verify project builds without errors
   - **Dependency Test:** Verify OCRazorModuleDemo can reference updated RazorDemo
   - **Component Test:** If possible, render Razor components to verify no runtime issues
   - **Integration Test:** Test via OCRazorModuleDemo and CmsWeb application

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] Microsoft.AspNetCore.Components.Web = 10.0.1
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Dependent project (OCRazorModuleDemo) builds successfully
   - [ ] No package restore errors

---

### DrewBrasher.OrchardCore.Blazor.csproj

**Path:** `DrewBrasher.OrchardCore.Blazor\DrewBrasher.OrchardCore.Blazor.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** ClassLibrary (Razor SDK)
- **Dependencies:** 0 project references
- **Dependants:** 1 (OCRazorModuleDemo)
- **LOC:** 253
- **Files:** 10
- **Risk Level:** ?? Low
- **Package Count:** 4
  - OrchardCore.Module.Targets 2.1.6
  - OrchardCore.ContentManagement 2.1.6
  - OrchardCore.ContentTypes.Abstractions 2.1.6
  - OrchardCore.DisplayManagement 2.1.6

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 0 (all compatible)

#### Migration Steps

1. **Prerequisites**
   - No project dependencies to migrate first
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `DrewBrasher.OrchardCore.Blazor\DrewBrasher.OrchardCore.Blazor.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Status |
   |---------|----------------|--------|
   | OrchardCore.Module.Targets | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.ContentManagement | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.ContentTypes.Abstractions | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.DisplayManagement | 2.1.6 | ? Compatible - no update needed |

   **No package reference changes required.**

4. **Expected Breaking Changes**
   
   **None.** Assessment shows:
   - 0 binary incompatible APIs
   - 0 source incompatible APIs
   - 0 behavioral changes in this project
   - 643 APIs analyzed, all compatible

5. **Code Modifications**
   
   **None expected.** OrchardCore packages are compatible and Blazor integration is stable.

6. **Testing Strategy**
   
   - **Build Test:** Verify project builds without errors
   - **Blazor Component Test:** Verify Blazor component rendering works
   - **OrchardCore Integration:** Test module loads in OrchardCore runtime
   - **Dependency Test:** Verify OCRazorModuleDemo can reference updated module

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] All PackageReferences unchanged (compatible)
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Dependent project (OCRazorModuleDemo) builds successfully
   - [ ] Module loads in CmsWeb application

---

### DrewBrasher.OrchardCore.ContentWarning.csproj

**Path:** `DrewBrasher.OrchardCore.ContentWarning\DrewBrasher.OrchardCore.ContentWarning.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** ClassLibrary
- **Dependencies:** 0 project references
- **Dependants:** 1 (CmsWeb)
- **LOC:** 262
- **Files:** 15
- **Risk Level:** ?? Low
- **Package Count:** 6
  - OrchardCore.Module.Targets 1.8.3
  - OrchardCore.ContentManagement 1.8.3
  - OrchardCore.ContentTypes.Abstractions 1.8.3
  - OrchardCore.DisplayManagement 1.8.3
  - OrchardCore.Infrastructure 1.8.3
  - OrchardCore.Shortcodes.Abstractions 1.8.3

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 0 (all compatible)

#### Migration Steps

1. **Prerequisites**
   - No project dependencies to migrate first
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `DrewBrasher.OrchardCore.ContentWarning\DrewBrasher.OrchardCore.ContentWarning.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Status |
   |---------|----------------|--------|
   | OrchardCore.Module.Targets | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.ContentManagement | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.ContentTypes.Abstractions | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.DisplayManagement | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.Infrastructure | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.Shortcodes.Abstractions | 1.8.3 | ? Compatible - no update needed |

   **No package reference changes required.**
   
   **Note:** This project uses OrchardCore 1.8.3 packages (older than 2.1.6 used in other projects). Assessment confirms compatibility with .NET 10.0.

4. **Expected Breaking Changes**
   
   **None.** Assessment shows:
   - 0 binary incompatible APIs
   - 0 source incompatible APIs
   - 0 behavioral changes in this project
   - 623 APIs analyzed, all compatible

5. **Code Modifications**
   
   **None expected.** ContentWarning module uses stable OrchardCore APIs with full compatibility.

6. **Testing Strategy**
   
   - **Build Test:** Verify project builds without errors
   - **Module Load Test:** Verify module loads in OrchardCore runtime
   - **Feature Test:** Test ContentWarningPart functionality
   - **Shortcode Test:** Verify ContentWarning shortcode provider works
   - **Display Driver Test:** Test ContentWarningPartDisplayDriver

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] All PackageReferences unchanged (compatible)
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Module loads in CmsWeb application
   - [ ] ContentWarning feature works as expected

---

### DrewBrasher.OrchardCore.ExternalData.csproj

**Path:** `DrewBrasher.OrchardCore.ExternalData\DrewBrasher.OrchardCore.ExternalData.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** ClassLibrary
- **Dependencies:** 0 project references
- **Dependants:** 1 (CmsWeb)
- **LOC:** 87
- **Files:** 4 (3 with incidents)
- **Risk Level:** ?? Low (with behavioral changes)
- **API Issues:** 2 behavioral changes
- **Package Count:** 6
  - OrchardCore.Module.Targets 1.8.3
  - OrchardCore.ContentManagement 1.8.3
  - OrchardCore.ContentTypes.Abstractions 1.8.3
  - OrchardCore.DisplayManagement 1.8.3
  - OrchardCore.Markdown.Abstractions 1.8.3
  - OrchardCore.Shortcodes.Abstractions 1.8.3

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 0 (all compatible)
- **Behavioral Changes:** 2 requiring runtime validation

#### Migration Steps

1. **Prerequisites**
   - No project dependencies to migrate first
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `DrewBrasher.OrchardCore.ExternalData\DrewBrasher.OrchardCore.ExternalData.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Status |
   |---------|----------------|--------|
   | OrchardCore.Module.Targets | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.ContentManagement | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.ContentTypes.Abstractions | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.DisplayManagement | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.Markdown.Abstractions | 1.8.3 | ? Compatible - no update needed |
   | OrchardCore.Shortcodes.Abstractions | 1.8.3 | ? Compatible - no update needed |

   **No package reference changes required.**

4. **Expected Breaking Changes**
   
   **Behavioral Changes (2):** These do not require code changes but need runtime validation.

   **Behavioral Change #1: HttpContent**
   - **API:** `System.Net.Http.HttpContent`
   - **Change:** Disposal behavior modified in .NET 9+
   - **Impact:** If code manually disposes HttpContent or relies on specific disposal timing, behavior may differ
   - **Location:** Likely in HTTP client usage for external data retrieval
   - **Validation Required:** Test external data fetching operations

   **Behavioral Change #2: AddHttpClient**
   - **API:** `Microsoft.Extensions.DependencyInjection.HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)`
   - **Change:** HTTP client factory registration behavior modified
   - **Impact:** If code relies on specific service registration order or lifetime behavior, may differ
   - **Location:** Likely in `Startup.cs` DI configuration
   - **Validation Required:** Test HTTP client injection and usage

5. **Code Modifications**
   
   **No immediate code changes expected.** Behavioral changes are typically backward-compatible with proper usage patterns. However, review these areas:

   **Areas to Review:**
   - HTTP client creation and disposal patterns
   - HttpContent lifecycle management
   - Dependency injection configuration for HTTP clients
   - External data retrieval operations

   **If issues arise during runtime testing:**
   - Ensure HttpContent is not manually disposed when using HttpClientFactory
   - Verify HttpClient is obtained from factory, not created directly
   - Check that HTTP client lifetime matches expected behavior

6. **Testing Strategy**
   
   - **Build Test:** Verify project builds without errors
   - **Module Load Test:** Verify module loads in OrchardCore runtime
   - **HTTP Client Test (Critical):** Test all HTTP client usage patterns
     - External data retrieval operations
     - HTTP request/response handling
     - Error handling for failed requests
   - **DI Test:** Verify HTTP client factory injection works
   - **Integration Test:** Test ExternalContent shortcode provider

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] All PackageReferences unchanged (compatible)
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Module loads in CmsWeb application
   - [ ] ? **HTTP client operations work correctly** (behavioral validation)
   - [ ] ? **External data retrieval succeeds** (behavioral validation)
   - [ ] ? **No HTTP client disposal errors** (behavioral validation)

---

### OCRazorModuleDemo.csproj

**Path:** `Demo\OCRazorModuleDemo\OCRazorModuleDemo.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** ClassLibrary
- **Dependencies:** 2 project references
  - RazorDemo
  - DrewBrasher.OrchardCore.Blazor
- **Dependants:** 1 (CmsWeb)
- **LOC:** 89
- **Files:** 7
- **Risk Level:** ?? Low
- **Package Count:** 4
  - OrchardCore.Module.Targets 2.1.6
  - OrchardCore.ContentManagement 2.1.6
  - OrchardCore.ContentTypes.Abstractions 2.1.6
  - OrchardCore.DisplayManagement 2.1.6

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 0 (all compatible)

#### Migration Steps

1. **Prerequisites**
   - ? RazorDemo migrated to net10.0
   - ? DrewBrasher.OrchardCore.Blazor migrated to net10.0
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `Demo\OCRazorModuleDemo\OCRazorModuleDemo.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Status |
   |---------|----------------|--------|
   | OrchardCore.Module.Targets | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.ContentManagement | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.ContentTypes.Abstractions | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.DisplayManagement | 2.1.6 | ? Compatible - no update needed |

   **No package reference changes required.**

4. **Expected Breaking Changes**
   
   **None.** Assessment shows:
   - 0 binary incompatible APIs
   - 0 source incompatible APIs
   - 0 behavioral changes in this project
   - 253 APIs analyzed, all compatible

5. **Code Modifications**
   
   **None expected.** This demo module aggregates RazorDemo and Blazor components. As long as dependencies are updated correctly, no code changes needed.

6. **Testing Strategy**
   
   - **Build Test:** Verify project builds without errors
   - **Dependency Test:** Verify references to RazorDemo and Blazor module resolve
   - **Module Load Test:** Verify module loads in OrchardCore runtime
   - **Integration Test:** Test aggregated Razor and Blazor components
   - **Demo Functionality:** Verify demo features work end-to-end

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] All PackageReferences unchanged (compatible)
   - [ ] ProjectReferences resolve to net10.0 versions
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Module loads in CmsWeb application
   - [ ] Demo Razor components render correctly
   - [ ] Demo Blazor components render correctly

---

### DrewBrasher.OrchardCore.CmsWeb.csproj

**Path:** `DrewBrasher.OrchardCore.CmsWeb\DrewBrasher.OrchardCore.CmsWeb.csproj`

#### Current State
- **Target Framework:** net8.0
- **Project Type:** AspNetCore Web Application
- **Dependencies:** 3 project references
  - OCRazorModuleDemo
  - DrewBrasher.OrchardCore.ContentWarning
  - DrewBrasher.OrchardCore.ExternalData
- **Dependants:** 0 (root application)
- **LOC:** 30
- **Files:** 16 (2 with incidents)
- **Risk Level:** ?? Low (with behavioral changes)
- **API Issues:** 1 behavioral change
- **Package Count:** 3
  - OrchardCore.Application.Cms.Targets 2.1.6
  - OrchardCore.Logging.NLog 2.1.6
  - Lombiq.HelpfulExtensions 11.0.0

#### Target State
- **Target Framework:** net10.0
- **Updated Packages:** 0 (all compatible)
- **Behavioral Changes:** 1 requiring runtime validation

#### Migration Steps

1. **Prerequisites**
   - ? OCRazorModuleDemo migrated to net10.0
   - ? DrewBrasher.OrchardCore.ContentWarning migrated to net10.0
   - ? DrewBrasher.OrchardCore.ExternalData migrated to net10.0
   - Verify .NET 10.0 SDK installed

2. **Project File Update**
   
   Update `DrewBrasher.OrchardCore.CmsWeb\DrewBrasher.OrchardCore.CmsWeb.csproj`:
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Package Updates**

   | Package | Current Version | Status |
   |---------|----------------|--------|
   | OrchardCore.Application.Cms.Targets | 2.1.6 | ? Compatible - no update needed |
   | OrchardCore.Logging.NLog | 2.1.6 | ? Compatible - no update needed |
   | Lombiq.HelpfulExtensions | 11.0.0 | ? Compatible - no update needed |

   **No package reference changes required.**

4. **Expected Breaking Changes**
   
   **Behavioral Change #1: UseExceptionHandler**
   - **API:** `Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(IApplicationBuilder, string)`
   - **Change:** Exception handler middleware behavior modified in .NET 9+
   - **Impact:** Error page routing or exception handling behavior may differ
   - **Location:** Likely in application startup configuration (`Program.cs` or `Startup.cs`)
   - **Validation Required:** Test exception handling paths and error page display

5. **Code Modifications**
   
   **No immediate code changes expected.** Behavioral change is typically backward-compatible. However, review:

   **Areas to Review:**
   - Exception handler middleware registration
   - Error page configuration
   - Exception handling routes

   **If issues arise during runtime testing:**
   - Verify exception handler path exists and is accessible
   - Check error page route configuration
   - Ensure middleware registration order is correct
   - Review error handling behavior matches expected

6. **Testing Strategy**
   
   - **Build Test:** Verify solution builds without errors
   - **Application Start Test:** Verify application starts without errors
   - **Module Load Test:** Verify all custom modules load:
     - OCRazorModuleDemo
     - ContentWarning
     - ExternalData
   - **Exception Handler Test (Critical):** Test exception handling
     - Trigger a handled exception
     - Verify error page displays correctly
     - Check error logging works
   - **Feature Test:** Test all module features work correctly
   - **End-to-End Test:** Navigate through application and verify functionality

7. **Validation Checklist**
   
   - [ ] Project file TargetFramework = net10.0
   - [ ] All PackageReferences unchanged (compatible)
   - [ ] All ProjectReferences resolve to net10.0 versions
   - [ ] `dotnet build` succeeds with 0 errors
   - [ ] `dotnet build` produces 0 warnings
   - [ ] Application starts successfully
   - [ ] All modules load without errors
   - [ ] ? **Exception handler works correctly** (behavioral validation)
   - [ ] ? **Error pages display correctly** (behavioral validation)
   - [ ] ContentWarning module features work
   - [ ] ExternalData module features work
   - [ ] OCRazorModuleDemo features work
   - [ ] Blazor components render correctly
   - [ ] No runtime exceptions or errors

---

## Package Update Reference

### Overview

Out of 15 total NuGet packages across the solution, **only 1 requires an update**. The remaining 14 packages (93.3%) are already compatible with .NET 10.0.

### Required Package Updates

| Package | Current Version | Target Version | Projects Affected | Update Reason |
|---------|----------------|----------------|-------------------|---------------|
| Microsoft.AspNetCore.Components.Web | 8.0.14 | 10.0.1 | RazorDemo | Framework alignment; recommended for .NET 10.0 compatibility |

### Compatible Packages (No Update Needed)

#### OrchardCore Packages - Version 2.1.6

These packages are used in newer projects and modules:

| Package | Version | Projects | Compatibility |
|---------|---------|----------|---------------|
| OrchardCore.Module.Targets | 2.1.6 | DrewBrasher.OrchardCore.Blazor<br/>OCRazorModuleDemo | ? Compatible with .NET 10.0 |
| OrchardCore.ContentManagement | 2.1.6 | DrewBrasher.OrchardCore.Blazor<br/>OCRazorModuleDemo | ? Compatible with .NET 10.0 |
| OrchardCore.ContentTypes.Abstractions | 2.1.6 | DrewBrasher.OrchardCore.Blazor<br/>OCRazorModuleDemo | ? Compatible with .NET 10.0 |
| OrchardCore.DisplayManagement | 2.1.6 | DrewBrasher.OrchardCore.Blazor<br/>OCRazorModuleDemo | ? Compatible with .NET 10.0 |
| OrchardCore.Application.Cms.Targets | 2.1.6 | DrewBrasher.OrchardCore.CmsWeb | ? Compatible with .NET 10.0 |
| OrchardCore.Logging.NLog | 2.1.6 | DrewBrasher.OrchardCore.CmsWeb | ? Compatible with .NET 10.0 |

#### OrchardCore Packages - Version 1.8.3

These packages are used in older modules (ContentWarning, ExternalData):

| Package | Version | Projects | Compatibility |
|---------|---------|----------|---------------|
| OrchardCore.Module.Targets | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning<br/>DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |
| OrchardCore.ContentManagement | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning<br/>DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |
| OrchardCore.ContentTypes.Abstractions | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning<br/>DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |
| OrchardCore.DisplayManagement | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning<br/>DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |
| OrchardCore.Infrastructure | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning | ? Compatible with .NET 10.0 |
| OrchardCore.Markdown.Abstractions | 1.8.3 | DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |
| OrchardCore.Shortcodes.Abstractions | 1.8.3 | DrewBrasher.OrchardCore.ContentWarning<br/>DrewBrasher.OrchardCore.ExternalData | ? Compatible with .NET 10.0 |

#### Third-Party Packages

| Package | Version | Projects | Compatibility |
|---------|---------|----------|---------------|
| Lombiq.HelpfulExtensions | 11.0.0 | DrewBrasher.OrchardCore.CmsWeb | ? Compatible with .NET 10.0 |

### Package Version Consistency Note

The solution uses two different OrchardCore package version lines:
- **2.1.6** - Used in newer projects (Blazor, OCRazorModuleDemo, CmsWeb)
- **1.8.3** - Used in older modules (ContentWarning, ExternalData)

Both versions are assessed as compatible with .NET 10.0. This version inconsistency is acceptable for the upgrade but may be considered for standardization in future updates (separate from this .NET version upgrade).

### Package Update Execution

Since this is an All-At-Once upgrade, package updates are applied atomically with framework updates:

1. Update RazorDemo.csproj:
   ```xml
   <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="10.0.1" />
   ```

2. Restore all packages:
   ```bash
   dotnet restore DrewBrasher.OrchardCore.sln
   ```

3. Verify no restore errors
4. Proceed to build

**No other package changes needed.**

---

## Breaking Changes Catalog

### Summary

The assessment identified **0 binary incompatible** and **0 source incompatible** API changes. The upgrade contains **only 3 behavioral changes** affecting 2 projects.

### Breaking Change Categories

| Category | Count | Impact Level | Action Required |
|----------|-------|--------------|-----------------|
| Binary Incompatible | 0 | None | No code changes needed |
| Source Incompatible | 0 | None | No code changes needed |
| Behavioral Changes | 3 | Low | Runtime validation required |

### Behavioral Changes Detail

Behavioral changes do not break compilation but may alter runtime behavior. All three changes are low-impact and typically backward-compatible with proper API usage.

---

#### Behavioral Change #1: UseExceptionHandler

**API:** `Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(IApplicationBuilder, string)`

**Affected Project:** DrewBrasher.OrchardCore.CmsWeb

**Description:** Exception handler middleware behavior has changed in .NET 9+ regarding how exception handling paths are resolved and invoked.

**Potential Impact:**
- Error page routing behavior may differ
- Exception context may be passed differently
- Middleware order dependency may be affected

**Recommended Actions:**
1. Test exception handling by triggering errors
2. Verify error pages display correctly
3. Check exception logging captures expected information
4. Review middleware registration order in Program.cs/Startup.cs

**Code Review Areas:**
- Exception handler middleware configuration
- Error page route configuration
- Custom exception handling logic

**Validation:**
- Trigger handled exceptions and verify error page displays
- Check application logs for proper exception capture
- Test both development and production error handling modes

**Likelihood of Issues:** Low (typically backward-compatible)

---

#### Behavioral Change #2: HttpContent

**API:** `System.Net.Http.HttpContent`

**Affected Project:** DrewBrasher.OrchardCore.ExternalData

**Description:** HttpContent disposal behavior has changed in .NET 9+. The timing and ownership of HttpContent disposal when using HttpClient may differ.

**Potential Impact:**
- Manual disposal of HttpContent may cause issues
- HttpContent lifetime with HttpClientFactory may behave differently
- Stream-based content reading patterns may need adjustment

**Recommended Actions:**
1. Review all HTTP client usage in ExternalData module
2. Verify HttpContent is not manually disposed when using HttpClientFactory
3. Test external data retrieval operations
4. Monitor for disposal-related exceptions

**Code Review Areas:**
- HTTP request creation and sending
- HttpContent lifecycle management
- Response content reading patterns

**Validation:**
- Test all external data fetching operations
- Verify no ObjectDisposedException errors
- Check HTTP responses are properly read

**Likelihood of Issues:** Low (proper HttpClientFactory usage should be unaffected)

---

#### Behavioral Change #3: AddHttpClient

**API:** `Microsoft.Extensions.DependencyInjection.HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)`

**Affected Project:** DrewBrasher.OrchardCore.ExternalData

**Description:** HTTP client factory registration behavior has changed. Service registration order, lifetime, or configuration may behave differently.

**Potential Impact:**
- HTTP client injection behavior may differ
- Service lifetime or scope may be affected
- Configuration delegation patterns may need adjustment

**Recommended Actions:**
1. Review AddHttpClient usage in Startup.cs
2. Verify HTTP clients are properly injected
3. Test HTTP client factory service resolution
4. Validate HTTP client configuration is applied

**Code Review Areas:**
- Dependency injection configuration for HTTP clients
- HTTP client factory registration
- HTTP client usage and injection points

**Validation:**
- Verify HTTP clients are successfully injected
- Test HTTP client operations work correctly
- Check HTTP client configuration is honored

**Likelihood of Issues:** Low (standard registration patterns should be unaffected)

---

### No Breaking Changes

The following categories have **zero** breaking changes:

**Binary Incompatible APIs:** None
- No APIs have been removed or have incompatible signatures
- No code changes required for binary compatibility

**Source Incompatible APIs:** None
- No compilation errors expected from API changes
- No source code modifications needed for compilation

### API Compatibility Statistics

| Project | Total APIs Analyzed | Binary Incompatible | Source Incompatible | Behavioral Changes | Compatible |
|---------|---------------------|---------------------|---------------------|-------------------|------------|
| RazorDemo | 84 | 0 | 0 | 0 | 84 (100%) |
| DrewBrasher.OrchardCore.Blazor | 643 | 0 | 0 | 0 | 643 (100%) |
| DrewBrasher.OrchardCore.ContentWarning | 623 | 0 | 0 | 0 | 623 (100%) |
| DrewBrasher.OrchardCore.ExternalData | 77 | 0 | 0 | 2 | 75 (97.4%) |
| OCRazorModuleDemo | 253 | 0 | 0 | 0 | 253 (100%) |
| DrewBrasher.OrchardCore.CmsWeb | 38 | 0 | 0 | 1 | 37 (97.4%) |
| **Total** | **1,718** | **0** | **0** | **3** | **1,715 (99.8%)** |

### Breaking Changes by Framework Area

No framework-specific breaking change patterns identified. The 3 behavioral changes are isolated to:
- ASP.NET Core middleware (UseExceptionHandler)
- HttpClient infrastructure (HttpContent, AddHttpClient)

### Migration Path Summary

**For Behavioral Changes:**
1. No code changes required upfront
2. Update framework as planned
3. Execute comprehensive runtime testing
4. Monitor for unexpected behavior
5. Apply fixes only if issues surface

**Expected Outcome:** All behavioral changes backward-compatible; no modifications needed.
