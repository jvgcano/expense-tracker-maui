# Feature Development Workflow

## Purpose

This document defines the standard workflow for developing a feature in the
Expense Tracker MAUI project.

The workflow is designed to keep changes isolated, reviewable, traceable,
and safe to merge into `main`.

## Workflow Overview

```text
main
  │
  ├── Create feature branch
  │
  ▼
feature/<feature-name>
  │
  ├── Implement change
  ├── Review working tree
  ├── Stage changes
  ├── Review staged changes
  ├── Commit
  └── Push
        │
        ▼
    Pull Request
        │
        ├── Review
        ├── Validate
        └── Rebase and merge
                │
                ▼
               main
                │
                ├── Delete remote feature branch
                ├── Update local main
                ├── Delete local feature branch
                ├── Prune remote references
                └── Final verification
```

## 1. Start from an Updated `main`

Always begin feature development from a clean and up-to-date `main` branch.

Switch to `main`:

```powershell
git switch main
```

Pull the latest changes:

```powershell
git pull origin main
```

Verify the working tree:

```powershell
git status
```

Expected result:

```text
On branch main
Your branch is up to date with 'origin/main'.

nothing to commit, working tree clean
```

### Why

Starting from an updated `main` ensures the feature branch is based on the
latest version of the application and reduces the likelihood of unnecessary
merge conflicts.

## 2. Create a Feature Branch

Create a dedicated branch for the feature or change.

Use the following naming convention:

```text
feature/<feature-name>
```

Example:

```powershell
git switch -c feature/expense-entry
```

Verify the active branch:

```powershell
git branch
```

The new feature branch should be the active branch before making changes.

### Why

Feature branches isolate work from `main`. This allows the change to be
developed, reviewed, tested, and merged independently.

## 3. Implement the Change

Make the required code and documentation changes for the feature.

Keep the change focused on the feature being developed.

Avoid mixing unrelated changes into the same feature branch.

### Guidelines

- Keep the scope of the branch focused.
- Follow the project's architecture and coding conventions.
- Add or update tests when applicable.
- Update relevant documentation when the change affects documented behavior.
- Build the solution after making significant structural changes.

The goal is to produce a complete, independently reviewable change rather than
a collection of unrelated modifications.

## 4. Review the Changes

Before staging changes, inspect the working tree to understand what has
changed.

Check the current repository state:

```powershell
git status
```

Review modifications to tracked files:

```powershell
git diff
```

Use the output to verify that the changes are related to the feature and that
no unrelated files have been modified.

### Why

Reviewing the working tree before staging helps catch:

- Unintended changes
- Unrelated modifications
- Generated files that should not be committed
- Missing files
- Changes made outside the intended feature scope

Do not stage or commit changes until the working tree has been reviewed.

## 5. Stage the Changes

Stage only the changes that belong to the feature.

For example:

```powershell
git add .
```

Check the staging state:

```powershell
git status
```

Review exactly what has been staged:

```powershell
git diff --cached
```

For a concise list of staged files:

```powershell
git diff --cached --name-status
```

### Why

The staging area provides an opportunity to review the exact changes that will
be included in the commit.

Do not commit until the staged changes have been reviewed.

## 6. Commit the Changes

Create a commit after reviewing the staged changes.

Use the project's conventional commit format:

```text
<type>: <description>
```

Example:

```powershell
git commit -m "feat: establish application architecture"
```

### Commit Types

Use a commit type that describes the nature of the change:

| Type | Purpose |
| ----------- | ----------- |
| `feat` | Introduces new functionality |
| `fix` | Fixes a defect |
| `docs` | Documentation-only changes |
| `chore` | Maintenance or repository changes |
| `refactor` | Code restructuring without changing behavior |
| `test` | Adds or changes tests |
| `build` | Changes to build configuration |
| `ci` | Changes to CI/CD configuration |

### Commit Message Guidelines
- Use the imperative form where practical.
- Keep the subject concise and descriptive.
- Describe the change rather than the implementation process.
- Prefer one logical change per commit.
- Do not include unrelated changes in the same commit.
- Follow the project's commit-message convention consistently.

### Why

Consistent commit messages make the repository history easier to understand,
review, search, and automate.

The project may later enforce the commit-message convention automatically
through CI/CD checks.

## 7. Push the Feature Branch

Push the feature branch to the remote repository.

For the first push of a new feature branch:

```powershell
git push -u origin feature/<feature-name>
```

Example:

```powershell
git push -u origin feature/expense-entry
```

The `-u` option establishes the upstream relationship between the local branch
and its remote counterpart.

After the upstream relationship has been established, subsequent pushes can
use:

```powershell
git push
```

Verify the branch exists on the remote repository after pushing.

### Why

Pushing the feature branch makes the work available on GitHub so that a Pull
Request can be created and the changes can go through the review and
validation process.

## 8. Create the Pull Request

Create a Pull Request on GitHub to merge the feature branch into `main`.

The Pull Request should:

- Target `main` as the base branch.
- Use a clear and descriptive title.
- Summarize the changes made.
- Describe important implementation decisions when applicable.
- Document validation that has been performed.
- Contain only changes relevant to the feature.

Example title:

```text
feat: establish application architecture
```

Example description:

```text
## Summary

- Establish the initial application architecture
- Create Domain, Application, Infrastructure, and MAUI projects
- Add project references according to the intended dependency direction

## Validation

- `dotnet build ExpenseTracker.slnx` passes successfully
```

### Why

The Pull Request is the review boundary between feature development and
main.

It provides a place to review the change before it becomes part of the main
development line.

## 9. Validate the Pull Request

Validate the changes before merging the Pull Request.

### Local Validation

Perform the appropriate validation locally before or while reviewing the Pull
Request.

For a .NET MAUI project, at minimum, verify that the solution builds:

```powershell
dotnet build ExpenseTracker.slnx
```

When automated tests are available, run them as well:

```powershell
dotnet test ExpenseTracker.slnx
```

Review the Pull Request on GitHub and verify:

- The intended changes are present.
- No unrelated changes are included.
- The Pull Request targets the correct base branch.
- The build succeeds.
- Tests pass when applicable.
- Any required documentation has been updated.

### Automated Validation

Automated validation will be introduced through GitHub Actions as the project's
CI/CD workflow evolves.

Planned automated checks may include:

- Commit message validation
- Solution build
- Automated tests
- Code quality checks
- Other project-specific validation

Automated checks should complement, rather than replace, developer review.

### Why

Validation provides confidence that the changes are safe to merge into `main`.

Local validation provides immediate feedback during development, while
automated CI validation provides a consistent and repeatable verification
process on GitHub.

## 10. Merge the Pull Request

After the Pull Request has been reviewed and all required validation has
passed, merge the Pull Request into `main`.

For this project, use:

**Rebase and merge**

### Why Rebase and Merge

Rebase and merge keeps the `main` branch history linear by applying the
feature commit(s) on top of the current `main` branch without creating an
additional merge commit.

For example, a feature branch may initially contain:

```text
main
  │
  └── feature commit
```

After a rebase-and-merge, the resulting history on `main` is:

```text
main
  │
  └── feature commit
```

The resulting commit may have a different commit ID from the original feature branch commit because GitHub recreates the commit on top of `main` as part of the rebase-and-merge operation.

### Important

After the Pull Request has been successfully merged, verify that GitHub shows
the Pull Request as merged before proceeding with branch cleanup.

### Why

Using a consistent merge strategy keeps the repository history predictable
and makes it easier to understand the sequence of changes on `main`.

## 11. Update Local `main`

After the Pull Request has been merged, switch back to the local `main`
branch:

```powershell
git switch main
```

Pull the latest changes from GitHub:

```powershell
git pull origin main
```

Verify the repository state:

```powershell
git status
```

Expected result:

```text
On branch main
Your branch is up to date with 'origin/main'.

nothing to commit, working tree clean
```

### Why

The Pull Request was merged on GitHub, so the local `main` branch must be
updated before continuing with local branch cleanup or starting the next
feature.

## 12. Delete the Remote Feature Branch

After the Pull Request has been successfully merged, delete the remote feature
branch from GitHub.

The remote branch is no longer needed because its changes have been merged
into `main`.

The branch can be deleted using the **Delete branch** option on the merged
Pull Request page.

### Why

Deleting merged feature branches keeps the remote repository clean and makes
active branches easier to identify.

## 13. Delete the Local Feature Branch

After the Pull Request has been merged and local `main` has been updated,
delete the local feature branch.

First verify the current branch:

```powershell
git branch
```

Make sure you are on `main` before deleting the feature branch.

Delete the local feature branch:

```powershell
git branch -d feature/<feature-name>
```

The `-d` option performs a safe deletion and warns if Git does not consider
the branch fully merged into the current branch.

### Why

The feature branch has completed its purpose after the changes have been
merged into `main`.

Deleting it locally keeps the repository clean and prevents old feature
branches from accumulating.

### Important

A warning may appear if the feature branch was merged using **Rebase and
merge**.

For example:

```text
warning: deleting branch 'feature/application-architecture' that has been
merged to 'refs/remotes/origin/feature/application-architecture', but not yet
merged to HEAD
```

`git branch -d` determines whether a branch is merged based on Git's commit graph.
With **Rebase and merge**, GitHub creates a new commit on `main`, so the original feature-branch commit may not be an ancestor of `main`.
Therefore, `git branch -d` may warn even though the Pull Request was successfully merged.

Before forcing deletion, verify that the Pull Request was successfully merged
and that local `main` has been updated.

Do not use `git branch -D` simply to suppress the warning.

## 14. Prune Stale Remote References

After deleting the remote feature branch, update the local repository's
knowledge of remote branches.

Run:

```powershell
git fetch --prune
```

This removes local remote-tracking references for branches that no longer
exist on the remote repository.

For example, Git may report:

```text
[deleted] (none) -> origin/feature/application-architecture
```

Verify the remaining branches:

```powershell
git branch -a
```

The deleted feature branch should no longer appear under `remotes/origin`.

### Why

Deleting a branch on GitHub does not automatically remove the corresponding
remote-tracking reference from every local clone.

`git fetch --prune` synchronizes the local view of remote branches with the
actual state of the remote repository.

## 15. Final Verification

Verify that the repository is clean and that local `main` matches the remote
repository.

Check the working tree:

```powershell
git status
```

Expected result:

```text
On branch main
Your branch is up to date with 'origin/main'.

nothing to commit, working tree clean
```

Review the local branches:

```powershell
git branch
```

Only the expected local branches should remain.

Review local and remote branches:

```powershell
git branch -a
```

Verify that deleted feature branches no longer appear under
`remotes/origin`.

Review the recent commit history:

```powershell
git log -3 --oneline
```

The latest merged change should be present on `main`.

### Why

Final verification confirms that the feature development cycle has been
completed successfully and that the repository is ready for the next feature.