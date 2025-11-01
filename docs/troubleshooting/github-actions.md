# Troubleshooting GitHub Actions

If you encounter issues with GitHub Actions workflows in this repository, consider the following troubleshooting steps:

## Permissions error executing Nuke build scripts
The build scripts may not have the correct execution permissions. To resolve this, you can set the appropriate permissions using the following command in your terminal:

```bash
git update-index --chmod=+x .\build.cmd
git update-index --chmod=+x .\build.sh
```

Solution found in: [nuke-build GitHub Issue 748](https://github.com/nuke-build/nuke/issues/748)