### Dim - coming soon

Dim will be a simple and easy to use database migration tool. It will be designed to help programmers get their databases under version control, just like their application code. 

Dim will be used along side version control tools like Git, Mercurial or Subversion. You choose.

### How to use

Dim comes will these commands:

```
$ dim help

Available commands are:

    backup      - Do a complete backup of the database
    baseline    - Save or execute a "baseline" script; used to create a database from scratch
    config      - Edit settings in your Dim config file
    init        - Initialise a new Dim project
    new         - Create a new script for updating your database
    routines    - Save or execute the routines for your database
    update      - Update your database with any changes shared by others

    help <name> - For help with one of the above commands
```

Create a new Dim project:

```
$ dim init -h localhost -P 3306 -u dim -p dim456 -s dim-tests

#### Initialising a new Dim project. ####
#
#    New config file created.
#    \dimconfig.json
#
#    New Dim project initialised!
#
```

Backup your database at any time:

```
$ dim backup

#### Running a complete backup ####
#
#  * New directory created: \.dim
#  * Tell your version control software to ignore the .dim directory.
#
#  * New directory created: \.dim\backups
#
#    Backup completed:
#    \.dim\backups\20121213-634909578024401843.sql
#
```

Create a 'baseline' from your database, for other developers to use.

```
$ dim baseline --save

#### Saving a new baseline script. ####
#
#  * New directory created: \dim\baseline
#
#  * New directory created: \dim\routines
#
#    Structure file:
#    \dim\baseline\structure.sql
#
#    Data file:
#    \dim\baseline\data.sql
#
#    Routines file:
#    \dim\routines\routines.sql
#
#    Saved! Now you can share changes with others.
#
```

Create a new script (patch) for changes you want to make to your database:

```
$ dim new --desc="update-table"

#### Creating a new file ####
#
#  * New directory created: \dim\patches
#
#    A new file has been created for you to use. Don't edit after you have shared it with others.
#    \dim\patches\20121213-634909582788484333-update-table.sql
#
```

Update your database with your new patch, or with changes shared by other developers:

```
$ dim update

#### Update the local database ####
#
#    1 new patch found.
#    1. \dim\patches\20121213-634909582788484333-update-table.sql
#
#    Backing up database (local backup)
#    Completed backup:
#    \.dim\backups\20121213-634909583053329481.sql
#
#    Executing: \dim\patches\20121213-634909582788484333-update-table.sql
#
#    Update completed.
#
```

Save your stored routines to a file:

```
$ dim routines --save

#### Saving a new routines script. ####
#
#    Routines Saved!
#    \dim\routines\routines.sql
#
```

Execute changes in your routines.sql file:

```
$ dim routines --execute

#### Executing the current routines script. ####
#
#    Backing up existing database first.
#
#    Backup complete:
#    \.dim\backups\20121213-634909583583899828.sql
#
#    Routines script executed!
#
```

Reset your database to the 'baseline':

```
$ dim baseline --execute

#### Executing the current baseline script. ####
#
#    Backing up existing database first.
#
#    Backup completed:
#    \.dim\backups\20121213-634909584894474789.sql
#
#    Baseline files executed:
#    \dim\baseline\structure.sql
#    \dim\baseline\data.sql
#
#    Routines file executed:
#    \dim\routines\routines.sql
#
#    1 new patch executed.
#    1. \dim\patches\20121213-634909582788484333-update-table.sql
#
```

Edit your config settings using 'dim config':

```
$ dim config -h 127.0.0.1 -P 3307

#### Setting fields in the local config file ####
#
#    'Host' has been set to '127.0.0.1'
#
#    'Port' has been set to '3307'
#
```

An example dimconfig.json file:

```
{
  "Host": "localhost",
  "Port": "3306",
  "Username": "dim",
  "Password": "dim456",
  "Schema": "dim-tests",
  "MySqlPath": "C:\\Program Files\\MySQL\\MySQL Server 5.5\\bin",
  "Patches": {
    "Path": "\\dim\\patches",
    "RunKind": "RunOnce"
  },
  "Routines": {
    "Path": "\\dim\\routines",
    "RunKind": "RunIfChanged"
  },
  "Baseline": {
    "Path": "\\dim\\baseline",
    "RunKind": "None"
  },
  "CustomFolders": [
    {
      "Path": "\\dim\\views",
      "RunKind": "RunAlways"
    },
    {
      "Path": "\\dim\\lookup-data",
      "RunKind": "RunIfChanged"
    }
  ]
}
```

