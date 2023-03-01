---
sidebar_position: 1
---

# Tutorial Intro

Let's discover **BrowserDir in less than 5 minutes**.

## Getting Started

Get started by going to the root of the site <a href="/">ROOT</a>

You will see a list of files and folders

![BrowserDir](/img/BrowserDir_firstPage.png)

### How to setup

Edit appsettings.json and add your folders to the entry

```
"FoldersToRead": [
    {
      "FullPath": "/",
      "Id": "root"
    },
    {
      "FullPath": "./",
      "Id": "Documents"
    }
  ]
```
You can add as many different entries that you want


