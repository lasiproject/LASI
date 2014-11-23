declare module LASI.Progress {
    class Status {
        message: string;
        percent: number;
        percentString: string;
        constructor(message: string, percent: number);
        static fromJson(jsonString: string): Status;
    }
}
