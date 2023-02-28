export interface FolderToRead {
    id:                string;
    fullPath:          string;
    transformFullPath: string;
    exists:            boolean;
    length:            number;
    physicalPath:      string;
    name:              string;
    lastModified:      Date;
    isDirectory:       boolean;
}
