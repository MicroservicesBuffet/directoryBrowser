import {
    QueryClient,
    QueryClientProvider,
    useQuery,
  } from '@tanstack/react-query'
import { useState } from 'react'
import { FolderToRead } from './genericFiles/FolderToRead';
import DisplayFoldersAndFiles from './justDisplayGUI/displayFoldersAndFiles';
  

  export default function DisplayFiles(){

    const fetchFolders = (): Promise<FolderToRead[]> =>
    fetch('http://localhost:5288/api/v1.0/File/GetRootFolders').then(
      (res) => res.json() ,
    )

    const { isLoading, error, data } = useQuery({
        queryKey: ['rootFolders'],
        queryFn: fetchFolders
      })
    
      if (isLoading) return <>'Loading...'</>
    
      if (error) return <>'An error has occurred: ' + JSON.stringify(error)</>
      

    return <>
      <DisplayFoldersAndFiles allData={data!} ></DisplayFoldersAndFiles>
    </>
  }