﻿//this was autogenerated by a tool. Do not modify! Use partial
 import DatabaseTableSelector, { SearchData } from "../../../DatabaseTableSelector";
 import JsonStreamDecoder from "../../../Common/asyncEnumerable";
 import useRxObs from "../../../../useRXEffect";
 import useSearchURL from "../../../searchMatches";
 import { Button,Input ,Spin, Table } from "antd";
 import { ColumnsType } from "antd/es/table";
 import { Link } from "react-router-dom";
 import { ajax } from "rxjs/ajax";
 import { concatMap, delay, map,Observable,of,scan } from "rxjs";
 import { useState,useEffect } from "react";



export class ApplicationDBContext_ModifiedUserFile_Table 
{

baseUrl:string = '';
    constructor(cc: Partial<ApplicationDBContext_ModifiedUserFile_Table> | null = null) {
      this.baseUrl=process.env.REACT_APP_URL+'api/'; 

    if (cc != null) {
      // Object.keys(tilt).forEach((key) => {
      //   (this as any)[key] = (tilt as any)[key];
      // });
      Object.assign(this,cc);
    }
    }

        public iduser : number  = 0;
                public idfile : number  = 0;
                public modifieddate : Date  = new Date();
                public contents : number[]  = [];
        }
export class ApplicationDBContext_ModifiedUserFile_Table_Interaction {
    baseUrl:string = '';
    constructor() {
      this.baseUrl=process.env.REACT_APP_URL+'api/'; 
    }

    
    public getAllCount():Observable<number>{

        var data=ajax.getJSON(this.baseUrl+'AdvancedSearch_ApplicationDBContext_ModifiedUserFile/GetAllCount')
        .pipe(
            map(response => {

                return response as number;
            })
            //takeUntil(cancel)
          );
          return data;
    }    
     public getSearchSimple(searchData:SearchData ):Observable<ApplicationDBContext_ModifiedUserFile_Table[]>{
        
        var url= this.baseUrl+'AdvancedSearch_ApplicationDBContext_ModifiedUserFile/';
        url+=`GetSearchSimple?ColumnName=${searchData.ColumnName}&Operator=${searchData.Operator}&Value=${searchData.Value}`;        
        var data=JsonStreamDecoder.fromFetchStream<ApplicationDBContext_ModifiedUserFile_Table>(url)                
        .pipe(
            map(it=>new ApplicationDBContext_ModifiedUserFile_Table(it)),                    
            concatMap((x:ApplicationDBContext_ModifiedUserFile_Table,index:number)=>{
              if((index+1) % 100 === 0)
              return of<ApplicationDBContext_ModifiedUserFile_Table>(x).pipe(delay(5*1000));
            else
              return of<ApplicationDBContext_ModifiedUserFile_Table>(x);
            }),
            
            scan((acc:ApplicationDBContext_ModifiedUserFile_Table[],value:ApplicationDBContext_ModifiedUserFile_Table)=>[...acc, value] as ApplicationDBContext_ModifiedUserFile_Table[], [] as ApplicationDBContext_ModifiedUserFile_Table[]),
            
          );
          return data as Observable<ApplicationDBContext_ModifiedUserFile_Table[]>;

    }

            public getAll():Observable<ApplicationDBContext_ModifiedUserFile_Table[]>{
                //var data=ajax.getJSON(this.baseUrl+'AdvancedSearch_ApplicationDBContext_ModifiedUserFile/GetAll')
                //.pipe(
                //    map(response => {        
                //        return response as ApplicationDBContext_ModifiedUserFile_Table[];
                //    })
                //  );
                                var data=JsonStreamDecoder.fromFetchStream<ApplicationDBContext_ModifiedUserFile_Table>(this.baseUrl+'AdvancedSearch_ApplicationDBContext_ModifiedUserFile/GetAll')                
                .pipe(
                    map(it=>new ApplicationDBContext_ModifiedUserFile_Table(it)),                    
                    concatMap((x:ApplicationDBContext_ModifiedUserFile_Table,index:number)=>{
                      if((index+1) % 100 === 0)
                      return of<ApplicationDBContext_ModifiedUserFile_Table>(x).pipe(delay(5*1000));
                    else
                      return of<ApplicationDBContext_ModifiedUserFile_Table>(x);
                    }),
                    
                    scan((acc:ApplicationDBContext_ModifiedUserFile_Table[],value:ApplicationDBContext_ModifiedUserFile_Table)=>[...acc, value] as ApplicationDBContext_ModifiedUserFile_Table[], [] as ApplicationDBContext_ModifiedUserFile_Table[]),
                    
                  );
                  return data as Observable<ApplicationDBContext_ModifiedUserFile_Table[]>;

            }
         }
let inputValueSearch_ModifiedUserFile = '';
export default function TableData_ModifiedUserFile() 
{
    const nameTable = 'ModifiedUserFile';
    const nameDB = 'ApplicationDBContext';
    const [loading,setLoading]= useState<boolean>(false);
    const [showAll, searchSimpleData]=useSearchURL();
    
    const interaction=new ApplicationDBContext_ModifiedUserFile_Table_Interaction();
    const [dataTable,setDataTable]= useState<ApplicationDBContext_ModifiedUserFile_Table[]|null>(null);
const [dataTableFiltered, setDataTableFiltered] = useState<ApplicationDBContext_ModifiedUserFile_Table[] | null>(null);
    
const columns : ColumnsType<ApplicationDBContext_ModifiedUserFile_Table> =[
    {
        title: 'ID',
        dataIndex: 'id',
        key: 'id',
        width: '5%',
        render:(item: any, record: ApplicationDBContext_ModifiedUserFile_Table, index: any)=>(<>{index+1}</>)
      }

     ,{
    title: 'IDUser',
    dataIndex: 'iduser',
    key: 'iduser',
    sorter: (a:ApplicationDBContext_ModifiedUserFile_Table, b:ApplicationDBContext_ModifiedUserFile_Table) => 
    {
        if(a.iduser == null && b.iduser == null)
            return 0;
        
        if(a.iduser == null)
            return -1;
        if(b.iduser == null)
            return 1;
                    return a.iduser - b.iduser;
            }
  } 

   ,{
    title: 'IDFile',
    dataIndex: 'idfile',
    key: 'idfile',
    sorter: (a:ApplicationDBContext_ModifiedUserFile_Table, b:ApplicationDBContext_ModifiedUserFile_Table) => 
    {
        if(a.idfile == null && b.idfile == null)
            return 0;
        
        if(a.idfile == null)
            return -1;
        if(b.idfile == null)
            return 1;
                    return a.idfile - b.idfile;
            }
  } 

   ,{
    title: 'ModifiedDate',
    dataIndex: 'modifieddate',
    key: 'modifieddate',
    sorter: (a:ApplicationDBContext_ModifiedUserFile_Table, b:ApplicationDBContext_ModifiedUserFile_Table) => 
    {
        if(a.modifieddate == null && b.modifieddate == null)
            return 0;
        
        if(a.modifieddate == null)
            return -1;
        if(b.modifieddate == null)
            return 1;
                
        return a.modifieddate.toString().localeCompare(b.modifieddate.toString());
            }
  } 

   ,{
    title: 'Contents',
    dataIndex: 'contents',
    key: 'contents',
    sorter: (a:ApplicationDBContext_ModifiedUserFile_Table, b:ApplicationDBContext_ModifiedUserFile_Table) => 
    {
        if(a.contents == null && b.contents == null)
            return 0;
        
        if(a.contents == null)
            return -1;
        if(b.contents == null)
            return 1;
                
        return a.contents.toString().localeCompare(b.contents.toString());
            }
  } 

  
];


    const filterData = (val: string, data: ApplicationDBContext_ModifiedUserFile_Table[]) => {
        if (val == null || val === '') {
            setDataTableFiltered(data);
            return;
        }
        if (data == null) {
            setDataTableFiltered(null);
            return;
        }
        val = val.toLowerCase();
        var f = data.filter(it => {
    if (it.iduser != null)
       if (it.iduser.toString().toLowerCase().includes(val))
        return true;

    if (it.idfile != null)
       if (it.idfile.toString().toLowerCase().includes(val))
        return true;

    if (it.modifieddate != null)
       if (it.modifieddate.toString().toLowerCase().includes(val))
        return true;

    if (it.contents != null)
       if (it.contents.toString().toLowerCase().includes(val))
        return true;

        return false;
        });
        setDataTableFiltered(f);
    }
    useEffect(()=>{
        document.title = nameTable+" - "+nameDB;
    })
    const [isLoadingNrRec, errorNrRect, nrRecords]= useRxObs(interaction.getAllCount());
    
    

    const showAllClickHandler=()=>{
        setDataTable(null);
        setLoading(true);
        interaction.getAll().subscribe({
          next: (data:ApplicationDBContext_ModifiedUserFile_Table[])=>{
                
                setDataTable(data);
                filterData(inputValueSearch_ModifiedUserFile, data);
            },
            complete:()=>{ setLoading(false);}
          }
        )
    };
    const searchSimple=(searchData: SearchData)=>{
        if(!searchData.IsValid()){
            window.alert("Invalid search data");
            return;
        }
        setDataTable(null);
        setLoading(true);
        interaction.getSearchSimple(searchData).subscribe({
          next: (data:ApplicationDBContext_ModifiedUserFile_Table[])=>{
                
                setDataTable(data);
                filterData(inputValueSearch_ModifiedUserFile, data);
            },
            complete:()=>{ setLoading(false);}
          }
        )

    };
    useEffect(()=>{
        if (typeof showAll==="boolean" &&  showAll){
            showAllClickHandler();
        }
        else{
            //console.log('basd',searchSimpleData,searchSimpleData != null && typeof searchSimpleData !== "boolean" && searchSimpleData.IsValid());
            if (searchSimpleData != null && typeof searchSimpleData !== "boolean" && searchSimpleData.IsValid())
                searchSimple(searchSimpleData);
        }
            
    // eslint-disable-next-line react-hooks/exhaustive-deps
    },[]);

    return (
    <>
        Table {nameTable} 
        {isLoadingNrRec && <Spin />} 
        {errorNrRect && <> - error loading data </>}
        {nrRecords != null && <> - {nrRecords} records</>}
        <p></p>
        <Button type="primary" loading={loading} onClick={showAllClickHandler}>Load All ModifiedUserFile</Button>
        <Link to="/Admin/Databases/ApplicationDBContext/tables/ModifiedUserFile/search/showall" target={"_blank"}>Direct Link</Link>

        <DatabaseTableSelector DBName={nameDB} TableName={nameTable} loadingData={loading} searchSimple={searchSimple}  />                    <div>
            {dataTable == null && "no data loaded"}
            {dataTable !=null &&             
                <>
                Number rows loaded {dataTable?.length} {loading && <Spin />} / filtered {dataTableFiltered?.length} / Search -{inputValueSearch_ModifiedUserFile}-
                {true /*!loading*/ &&
                            <Input placeholder="SearchHere" onChange={(e) => {
                                inputValueSearch_ModifiedUserFile=(e.target.value);
                                filterData(inputValueSearch_ModifiedUserFile, dataTable!);
                            }
                            } />
                        }
                        
  <Table pagination= {false} dataSource={dataTableFiltered!} columns={columns} />;
  </>
            }
        </div>

    </>
    )
}
