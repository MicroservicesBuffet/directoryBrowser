


# ApplicationDBContext

```mermaid
erDiagram
    ModifiedFile {
      
      long IDFile 
      
      string FullPathFile 
    
    }
    ModifiedUser {
      
      long IDUser 
      
      string NameUser 
    
    }
    ModifiedUserFile {
      
      long IDUser 
      
      long IDFile 
      
      DateTime ModifiedDate 
      
      long ID 
      
      byte-Array Contents 
    
    }
    ModifiedUserFile }o--|| ModifiedFile : FK_ModifiedUserFile_ModifiedFile
    ModifiedUserFile }o--|| ModifiedUser : FK_ModifiedUserFile_ModifiedUser
```
