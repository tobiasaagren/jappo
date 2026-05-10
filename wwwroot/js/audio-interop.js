window.jappoAudio = {
    speak: (text) => {
        if (!window.speechSynthesis) return;
        speechSynthesis.cancel();
        const u = new SpeechSynthesisUtterance(text);
        u.lang = 'ja-JP';
        u.rate = 0.9;
        speechSynthesis.speak(u);
    }
};
