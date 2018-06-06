# Project Information
* ProjectInfo.pdf	- Project information

# Documentation
* Documentation.eap	- Architecture and design diagrams
* Documentation.pdf	- Architecture and design diagrams in PDF format

# Client Configuration:
* WCF Client

# Server Configuration:
* Database connection
* WCF Service
* Repository types
	- Live			- Uses the provided SQL database
	- TestValid		- Basic implementation, returns valid values, used for testing
	- TestInvalid	- Basic implementation, returns invalid values, used for testing
* Report generator types
	- Live			- Uses IronPDF to generate documents
	- TestValid		- Basic implementation, returns valid values, used for testing
	- TestInvalid	- Basic implementation, returns invalid values, used for testing

# Known Issues
* Changing the service component causes the repository to return an Outage without executed actions

# June 5, 2018 Changelog
* In Outage entity changed Actions from a HashSet to a List
* Fixed Outage updating to include all properties
* Made Voltage Level and State readonly in Outage details window
* Added a confirmation dialog on creating and updating an Outage
* Changed the message that opens on empty search results from warning to information
* Added automatic search refresh after creating or updating an Outage