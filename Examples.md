Initialise a new Dim instance
=============================

	$ dim init -d 'mysql' -s 'test' -u 'root'


Update local database after pulling from others
===============================================


	$ dim update
	
	## 2 migration scripts found ##
	* '2012110-00000001-new-comments.sql'
	* '2012110-00000002-authors.sql'
	
	* Backing up existing database image
	* Running '2012110-00000001-new-comments.sql'
	* Running '2012110-00000002-authors.sql'
	
	* Update Completed!
	
	$ boom!


Create a new migration script
=============================

	$ dim script -t 'new-feature'
	
	## New script completed. Use this, and do not edit it after you have shared it with others ##
	* '20121101-0000003-new-feature.sql'
	
	$ vim dim-files\20121101-0000003-new-feature.sql













