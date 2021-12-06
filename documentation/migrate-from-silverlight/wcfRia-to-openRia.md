# Migrating a Silverlight app which uses Wcf-RiaServices

## Steps

### 1- 
First you need to make sure Wcf-RiaServices are installed or Wcf-RiaServices nuget packages are added.

### 2-
Build and run you silverlight solution/project and make sure it's building and starting and all features are running fine.

### 3-
Replace all Wcf-RiaServices with OpenRiaServices, removing references or nuget packages and add the OpenRiaServices packages

## 4-
Build and run you silverlight solution/project now with the OpenRiaServices and make sure it's building and starting and all features are running fine.

## 5- 
	Now proceed with rest of your migration steps to OpenSilver