declare module LASI.Progress {
    class Status {
        message: string;
        percent: number;
        id: number;
        percentString: string;
        constructor(message: string, percent: number, id?: number);
        static fromJson(jsonString: string): Status;
    }
}
