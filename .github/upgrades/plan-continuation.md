# Testing & Validation Strategy - Continuation

## Testing & Validation Strategy

### Multi-Level Testing Approach

Testing follows a multi-level strategy aligned with the All-At-Once upgrade approach:

1. **Per-Project Build Validation** - Ensure each project compiles
2. **Solution-Wide Build Validation** - Ensure entire solution builds
3. **Behavioral Change Validation** - Test runtime behavior of affected APIs
4. **Module Integration Validation** - Test module integration in CmsWeb application
5. **End-to-End Validation** - Comprehensive application testing

### Level 1: Per-Project Build Validation

Execute builds in dependency order (leaf ? intermediate ? root):

```bash
dotnet build Demo\RazorDemo\RazorDemo.csproj
dotnet build DrewBrasher.OrchardCore.Blazor\DrewBrasher.OrchardCore.Blazor.csproj
dotnet build DrewBrasher.OrchardCore.ContentWarning\DrewBrasher.OrchardCore.ContentWarning.csproj
dotnet build DrewBrasher.OrchardCore.ExternalData\DrewBrasher.OrchardCore.ExternalData.csproj
dotnet build Demo\OCRazorModuleDemo\OCRazorModuleDemo.csproj
dotnet build DrewBrasher.OrchardCore.CmsWeb\DrewBrasher.OrchardCore.CmsWeb.csproj
```

**Success Criteria:** All projects build with 0 errors, 0 warnings

### Level 2: Solution-Wide Build Validation

```bash
dotnet build DrewBrasher.OrchardCore.sln --configuration Debug
dotnet build DrewBrasher.OrchardCore.sln --configuration Release
dotnet clean DrewBrasher.OrchardCore.sln
dotnet build DrewBrasher.OrchardCore.sln --no-incremental
```

**Success Criteria:** Debug and Release builds succeed; clean build succeeds

### Level 3: Behavioral Change Validation

**Test 1: UseExceptionHandler (CmsWeb)**
- Start CmsWeb application
- Trigger handled exception
- Verify error page displays
- Check logs capture exceptions

**Test 2: HttpContent & AddHttpClient (ExternalData)**
- Start CmsWeb application
- Exercise HTTP client features in ExternalData module
- Verify external data operations succeed
- Monitor for disposal exceptions

**Success Criteria:** All behavioral tests pass without runtime errors

### Level 4: Module Integration Validation

Test all custom modules load and function:
- ContentWarning features
- ExternalData features  
- OCRazorModuleDemo features
- Blazor component rendering
- Razor component rendering

**Success Criteria:** All modules load and function correctly

### Level 5: End-to-End Validation

Comprehensive smoke tests:
1. Application starts successfully
2. Navigation works
3. Content management works
4. All module features accessible
5. Admin functions work

**Success Criteria:** All smoke tests pass

---

## Source Control Strategy

### Branch Strategy

- **Source Branch:** `main`
- **Upgrade Branch:** `upgrade-to-NET10` (already created and active)
- **Remote:** https://github.com/DrewBrasher/OrchardCoreModules

### Recommended Commit Strategy: Single Atomic Commit

**Commit Message Template:**
```
Upgrade solution from .NET 8.0 to .NET 10.0 (LTS)

- Updated all 6 projects to net10.0:
  * RazorDemo
  * DrewBrasher.OrchardCore.Blazor
  * DrewBrasher.OrchardCore.ContentWarning
  * DrewBrasher.OrchardCore.ExternalData
  * OCRazorModuleDemo
  * DrewBrasher.OrchardCore.CmsWeb

- Package Updates:
  * Microsoft.AspNetCore.Components.Web: 8.0.14 ? 10.0.1 (RazorDemo)

- Validation:
  * All builds successful (0 errors, 0 warnings)
  * 3 behavioral API changes validated and working
  * All modules load and function correctly
  * End-to-end testing passed

- No breaking changes required
- All 14 OrchardCore packages compatible without updates
```

**Commit Execution:**
```bash
git add .
git status  # Review changes
git commit -m "[commit message above]"
```

### Pre-Merge Checklist

- [ ] All projects build successfully
- [ ] All tests pass
- [ ] Behavioral changes validated
- [ ] Module integration tested
- [ ] End-to-end smoke tests passed
- [ ] No unexpected warnings or errors
- [ ] Commit message is clear and comprehensive
- [ ] Changes reviewed

### Merge Strategy

**Option 1: Merge Commit (Recommended)**
```bash
git checkout main
git merge upgrade-to-NET10 --no-ff
git push origin main
```

**Option 2: Squash Merge**
```bash
git checkout main
git merge upgrade-to-NET10 --squash
git commit -m "[comprehensive message]"
git push origin main
```

### Rollback Strategy

**Before Merge:**
```bash
git reset --hard origin/upgrade-to-NET10
```

**After Merge:**
```bash
git revert -m 1 <merge-commit-hash>
```

---

## Success Criteria

### Technical Criteria

**Framework Migration:**
- ? All 6 projects target net10.0
- ? No projects remain on net8.0
- ? TargetFramework property correct in all .csproj files

**Package Updates:**
- ? Microsoft.AspNetCore.Components.Web upgraded to 10.0.1 in RazorDemo
- ? All other 14 packages remain at compatible versions
- ? No package version conflicts
- ? All package references restore successfully

**Build Success:**
- ? All 6 projects build individually without errors
- ? Complete solution builds without errors
- ? Debug and Release configurations build successfully
- ? Clean build succeeds
- ? Zero compiler errors
- ? Zero compiler warnings

**Dependency Resolution:**
- ? All project references resolve to net10.0 versions
- ? OCRazorModuleDemo correctly references net10.0 dependencies
- ? CmsWeb correctly references net10.0 modules
- ? No dependency version conflicts

**API Compatibility:**
- ? No binary incompatible API issues
- ? No source incompatible API issues
- ? 3 behavioral changes validated and working

**Security:**
- ? No security vulnerabilities introduced
- ? No security vulnerabilities remain

### Quality Criteria

**Code Quality:**
- ? No code changes required (or only minimal for behavioral issues)
- ? Code structure maintained
- ? No technical debt introduced

**Test Coverage:**
- ? All existing functionality tested and working
- ? No regressions in features
- ? Module integration validated

**Documentation:**
- ? Upgrade documented in commit message
- ? Plan.md completed and accurate
- ? Breaking changes catalog comprehensive

### Process Criteria

**All-At-Once Strategy:**
- ? All 6 projects upgraded simultaneously
- ? No multi-targeting required
- ? Single coordinated operation completed
- ? No intermediate states

**Testing Executed:**
- ? Per-project build validation completed
- ? Solution-wide build validation completed
- ? Behavioral change validation completed:
  - UseExceptionHandler tested in CmsWeb
  - HttpContent behavior tested in ExternalData
  - AddHttpClient behavior tested in ExternalData
- ? Module integration testing completed
- ? End-to-end smoke testing completed

**Source Control:**
- ? Changes committed to upgrade-to-NET10 branch
- ? Commit message is clear and comprehensive
- ? All changes reviewed
- ? Ready for merge to main

### Runtime Validation Criteria

**Application Startup:**
- ? CmsWeb application starts without errors
- ? All custom modules load successfully
- ? No initialization errors in logs

**Module Functionality:**
- ? ContentWarning module works correctly
- ? ExternalData module works correctly
- ? OCRazorModuleDemo module works correctly
- ? Blazor components render correctly
- ? Razor components render correctly

**Exception Handling:**
- ? UseExceptionHandler middleware works correctly
- ? Error pages display as expected
- ? Exception logging captures errors

**HTTP Client Operations:**
- ? HTTP clients inject successfully
- ? External HTTP operations complete
- ? No HttpContent disposal errors
- ? HTTP client factory configuration honored

**OrchardCore Integration:**
- ? OrchardCore runtime stable
- ? Content management works
- ? Admin panel accessible
- ? No module initialization failures

### Definition of Done

**The .NET 10.0 upgrade is complete when:**

1. ? **All Technical Criteria met**
2. ? **All Quality Criteria met**
3. ? **All Process Criteria met**
4. ? **All Runtime Validation Criteria met**
5. ? **Changes merged to main branch**
6. ? **Upgrade branch tagged or cleaned up**

### Success Metrics

**Expected Outcomes Achieved:**
- ? Solution now targets .NET 10.0 LTS
- ? All OrchardCore modules compatible and functional
- ? Zero breaking code changes required
- ? 1 package successfully updated
- ? 14 packages compatible without changes (93.3%)
- ? 3 behavioral changes validated and working
- ? 1,718 APIs analyzed with 99.8% compatibility
- ? Zero security vulnerabilities
- ? Completed in single development session (All-At-Once)

**Upgrade is SUCCESSFUL and COMPLETE** when all above criteria are met.
