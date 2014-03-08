/// <reference path="../../typings/jquery/jquery.d.ts" />
interface VerbalContextInfo {
    subjects: number[];
    directObjects: number[];
    indirectObjects: number[];
}
declare var GetInfoForVerbal: (rawData: string) => VerbalContextInfo;
