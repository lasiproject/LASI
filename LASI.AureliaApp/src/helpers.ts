export class WindowService {
    sessionStorage: typeof window.sessionStorage = window.sessionStorage;
}

export function getHostElement() {
    return document.getElementById('app');
}