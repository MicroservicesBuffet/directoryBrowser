﻿//this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeneratorFromDB;
using Microsoft.EntityFrameworkCore;

namespace Generated;

//ADDED by code generator
public interface I_ModifiedUser_Table 
{
        long IDUser { get; set; }
                string NameUser { get; set; }
        }

public class ModifiedUser_Table : I_ModifiedUser_Table
{
    public static MetaTable metaData = new("ModifiedUser");
    static ModifiedUser_Table (){
        MetaColumn mc=null;
        mc=new ("IDUser","long",false);                
        mc.IsPk = true ;
        mc.TypeJS = "number";
        metaData.AddColumn(mc);
        mc=new ("NameUser","string",false);                
        mc.IsPk = false ;
        mc.TypeJS = "string";
        metaData.AddColumn(mc);
 //done with foreach property in static constructor
    }
        public long IDUser { get; set; }
                public string NameUser { get; set; }
             public void CopyFrom(I_ModifiedUser_Table other)  {
        this.IDUser = other.IDUser;
                this.NameUser = other.NameUser;
            }

    public static explicit operator ModifiedUser_Table?(ModifiedUser obj) { 
        if(obj == null)
            return null;
            //System.Diagnostics.Debugger.Break();
         var ret= new ModifiedUser_Table();
         ret.CopyFrom(obj as I_ModifiedUser_Table );
         return ret;
     }
     public static explicit operator ModifiedUser?(ModifiedUser_Table obj) { 
        if(obj == null)
            return null;
            //System.Diagnostics.Debugger.Break();
         var ret= new ModifiedUser();
         ret.CopyFrom(obj as I_ModifiedUser_Table) ;
         return ret;
     }



}
public partial class ModifiedUser : I_ModifiedUser_Table
{
     public void CopyFrom(I_ModifiedUser_Table other)  {
        this.IDUser = other.IDUser;
                this.NameUser = other.NameUser;
            }

}

//for ModifiedUser 
public enum eModifiedUserColumns {
    None = 0
        ,IDUser 
                ,NameUser 
        }

//finish ADDED by code generator


public partial class ModifiedUser
{
    [Key]
    public long IDUser { get; set; }

    [StringLength(500)]
    public string NameUser { get; set; } = null!;

    [InverseProperty("IDUserNavigation")]
    public virtual ICollection<ModifiedUserFile> ModifiedUserFile { get; } = new List<ModifiedUserFile>();
}

