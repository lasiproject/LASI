declare module LASI.Progress {
    class Status {
        public message: string;
        public percent: number;
        public formattedPercent: string;
        constructor(message: string, percent: number);
        static fromJson(jsonString: string): Status;
    }
}
