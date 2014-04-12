declare module LASI.Progress {
    class Status {
        public message: string;
        public percent: number;
        public percentString: string;
        constructor(message: string, percent: number);
        static fromJson(jsonString: string): Status;
    }
}
