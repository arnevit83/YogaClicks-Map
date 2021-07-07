
//16/07/2015



var questions = [
"Do you want to get cardio fit?",
"Do you think you’d like to flow in the fast lane?",
"Do you like a set routine?",

"How about some chilled, gentle moves?",
"Do you want to flow with the energy of the universe?",
"Do you like to work a Zen move every now and Zen?",

"Do you want the benefits without having to move?",
"Are you in pain?",
"Do you want a class where you can sit down?",
"Do you want to be able to lie down?",
"Do you want one to one help,<br />just you and the teach?",

"Do you like it hot?",
"Are you happiest on the water?",
"Do you wish you were hanging in your hammock?",
"Are you thinking Ibiza, glo-sticks at the ready?",

"Would you like to experience 101 uses of a chair?",
"Do you love lying over bolsters?",
"Would you like to use another person as a prop?",
"Do you believe heaven is a wall of ropes?",
"Do you think you'd like backbending on a specially constructed bench?",
"Do you like to look in mirrors?",

"Would you like to still the chatter in your mind?",
"How about some deep breathing?",
"Do you like a mantra, like a mantra, like a mantra?",
"Do you want to talk about it? Use yoga as therapy?",

"Do you want to open your heart?",
"Would you like to chant or sing?",
"Would you prefer a more traditional Indian approach?",
"Do you like debating the meaning of life?",
"In the words of David Bowie,<br />are you ready for ch-ch-ch-changes?",
"How about using your hands to move energy and create calm?",


"Are you pregnant?",
"Do you want to bring your baby?",
"Do you want to bring your toddler?",

"Do you want a Divine child?",
"Do you have a kid with special needs?",
"Do you want a chilled teen?",

"Do you believe you can fly?",
"Would you like to join the circus?",
"Do you float like a butterfly and sting like a bee?",
"Are you a whole lot of curves?",
"Do you like a mash up?",
"Are your headphones actually part of your ears?",
"Do you like precision, the planets in alignment?",
"Would you like to look ten years younger?",
"Do you like hanging upside down?"
];

var styles = [
"Acroyoga",
"Aerial",
"Ananda",
"Anusara",
"Ashtanga/ Mysore",
"Baptiste Power Vinyasa Yoga",
"Bhakti",
"Bihar/ Satyananda",
"Bikram",
"Bodhiyoga",
"Boxing yoga",
"Chair yoga",
"Core strength",
"Critical alignment",
"Curvy ",
"Desikachar/ Viniyoga",
"Dharma Yoga",
"Dru",
"Face",
"Forrest",
"Freedom Style Yoga",
"Gentle",
"Hatha",
"Himalayan Yoga Tradition",
"Holistic",
"Hot",
"Integral",
"Integrated Yoga & Mindfulness",
"Integrative Yoga Therapy",
"ISHTA",
"Iyengar",
"Japa",
"Jivamukti",
"Jnana",
"Kids",
"Kirtan",
"Kripalu",
"Kriya",
"Kundalini",
"Meditation",
"Moksha",
"Mudra",
"Mum & Baby",
"Mum & Toddler",
"Nidra",
"Okido",
"Partner",
"Phoenix Rising Yoga Therapy",
"Post Natal",
"Power Yoga",
"Prana Flow",
"Pranayama",
"Pre-natal",
"Raja",
"Restorative",
"Scaravelli",
"Seniors",
"Shadow",
"Sivananda",
"Special yoga",
"Stand up paddleboard yoga",
"Sun Power",
"Svaroopa",
"Svastha",
"Tantra",
"Teens",
"Tibetan",
"Trauma Sensitive",
"Triyoga",
"Vinyasa Flow",
"Windfire",
"Yin ",
"Yin Yang",
"Yoga & Pilates",
"Yoga & Qigong",
"Yoga for sports",
"Yoga raves",
"Yoga therapy"
];
var topics = [
"Are you mainly in this to get super-fit?",
"Do you want to flow, but slow?",
"Are you not so much into moving?",
"Do you want to holiday in the yoga studio?",
"Do you like a prop?",
"Do you need some head space?",
"Would you like to merge with the pool of cosmic bliss that is the universe?",
"Are you a mama/mama to be?",
"Do you have kids on board?",
"Do you like to zig where others zag, <br />can we surprise you?"
];

var topicimages = [
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/405c2aff-f1be-416a-852e-1774aae4e707.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/5b1995bf-631f-459b-bfdd-722abc73e7a9.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/9501e240-5fa2-4dca-ba50-28690283ac3e.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/de213c2a-e25b-4c49-abb1-95a73a1218d0.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3799f55f-cc7d-40b4-9268-262b66aa1ac5.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/25811371-8b77-400e-88ca-e9edbe781dbb.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/6721e4f2-cfe2-4273-9d67-0a29c810096d.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/d804d2f9-324b-45af-9712-69a7f0c9b2cc.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/97d490b6-6a23-47d7-acbf-06df6faa6a24.jpg",
    "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/99d4b2e3-a710-45b7-921c-fce054a22f46.jpg"
];

var questiontopics = [0, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9
];
var questiontopicsletters = ['a', 'b', 'c', 'a', 'b', 'c', 'a', 'b', 'c', 'd', 'e',  'a', 'b', 'c', 'd', 'a', 'b', 'c', 'd', 'e', 'f', 'a', 'b', 'c', 'd', 'a', 'b', 'c', 'd', 'e', 'f', 'a', 'b', 'c', 'a', 'b', 'c', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k'];







var answerpoints=[

[0,0,0,0,0,0,0,0,0,20,0,0,0,20,0,0,0,0,0,10,0,0,0,20,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,0,0,-100,0,0,1,-100,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,-20,0,-20,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,20,0,0,0,-20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-20,0,0,0,10,0,10,0,0,0,0,0,10,0,0,0,10,0,0,0],
[1,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,-20,0,-20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,-20,0,10,0,20,0,10,0,0,0,0,0,10,0,0,0,10,0,10,0],
[2,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,0,0,0,0,0,0],
[3,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,20,0,10,0,20,1,0,0,0,0,0,0,10,0,10,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,0,0,0,0,10,0,5,0,10,0,0,0,10,0],
[4,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,0,0,10,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0],
[5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,20,0,20,0,0,0,10,0,0,0,0,0],
[6,-100,0,-100,0,0,0,0,0,-100,0,0,0,-100,0,0,0,0,0,-100,0,0,0,-100,0,20,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,10,0,0,1,10,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,10,0,0,0,10,0,10,0,10,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,10,0,20,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0],
[7,-100,0,-100,0,0,0,0,0,-100,0,0,0,-100,0,0,0,0,0,-100,0,0,0,-100,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,1,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,-20,0,0,0,0,0,0],
[8,-100,0,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,-100,0],
[9,-100,0,-100,0,0,0,0,0,-100,0,0,0,-100,0,0,0,0,0,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,40,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,30,0,0,0,20,0,0,0,0,0,0,0,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,-100,0,-100,0],
[10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[11,0,0,0,0,0,0,0,0,0,0,0,0,30,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[12,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[13,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[14,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0],
[15,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[16,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,10,0,0,0,0,0,0,0,0,0],
[17,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[18,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[19,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[21,0,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,10,0,0,0,20,1,10,0,0,0,0,0,10,0,10,0,10,0,10,0,0,0,10,0,10,0,10,0,0,0,10,0,10,0,10,0,10,0,50,0,0,0,10,0,0,0,0,0,20,0,10,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,10,0,10,0,0,0,10,0,0,0,10,0,10,0,0,0,10,0,0,0,10,0],
[22,0,0,0,0,10,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,20,0,0,0,0,20,0,0,10,0,0,0,0,0,20,1,10,0,0,0,0,0,20,0,20,0,20,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,10,0,30,0,0,0,0,0,0,0,0,0,20,0,10,0,0,0,10,0,0,0,0,0,0,0,50,0,0,0,0,0,20,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,20,0,15,0,20,0,0,0,0,0,10,0,5,0,10,0,0,0,0,0],
[23,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[24,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,0,0,1,0,0,0,0,0,0,10,0,10,0,20,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[25,0,0,0,0,10,0,50,0,0,0,0,0,0,0,20,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,1,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,10,0,10,0,0,0,10,0,10,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[26,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,50,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[27,0,0,0,0,10,0,0,0,10,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,1,10,0,0,0,0,0,10,0,0,0,0,0,10,0,10,0,0,0,0,0,10,0,0,0,10,0,0,0,10,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[28,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,1,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,30,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[29,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,10,0,0,0,0,1,0,0,10,0,0,0,10,0,10,0,10,0,10,0,0,0,0,0,20,0,10,0,0,0,0,0,10,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[30,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[31,0,0,0,0,0,0,0,0,0,0,100,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,100,-100,0,0,0,0,0,0,-20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[32,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-10,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,1,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,100,-100,-20,0,-20,0,-20,0,-20,0,-20,0,100,-100,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0],
[33,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-10,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,1,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,0,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,100,-100,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0],
[34,-20,0,-20,0,-20,0,-20,0,-20,0,0,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,1,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,100,-100,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0],
[35,-20,0,-20,0,-20,0,-20,0,-20,0,0,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,1,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,0,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,100,-100,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0,-20,0],
[36,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,-20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,100,-100,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[37,20,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0],
[38,50,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[39,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0],
[40,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[41,10,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,10,0,10,0,10,0,10,0],
[42,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,10,0],
[43,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,10,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,20,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[44,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,50,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[45,0,0,50,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]
];




var stylescomplete = [
["Acroyoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/81399ce0-4340-4e35-b7ef-57a43c1903d6.jpg?width=527&height=277?width=527&height=277", "/styles/acroyoga/83", " AcroYoga blends yoga and acrobatics to form a practice that builds awareness, focus, connectivity and a sense of fun. The practice includes asana/poses, flying, inversions and partner acrobatics and is performed mainly in pairs, or in small groups."],
["Aerial", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/99d4b2e3-a710-45b7-921c-fce054a22f46.jpg?width=527&height=277", "/styles/aerial/138", " Created by Michelle Dortignac in 2006, Aerial Yoga helps achieve correct postural alignment using large slings which suspend the student off the floor - think hammock. This relieves pressure on the body and creates a relaxed practice. It allows the student to hold postures for longer as well as assisting with advanced inversions. Suitable for people with decreased joint mobility as pressure is relieved. It can also help to decompress the spine."],
["Ananda", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/86545d03-a42c-4963-b3ca-d8cc65c5884f.jpg?width=527&height=277", "/styles/ananda/70", " Founded in 1968 by Swami Kriyananda, from the teachings of Paramhansa Yogananda, Ananda is a gentle, inwardly directed physical, mental and spiritual approach. It pairs asana/poses with internal affirmations and uses unique 'Energization Exercises.' Ananda teachers don't see asana as fixed poses, instead asana are fitted to the needs and abilities of each student."],
["Anusara", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/932aeee6-f579-4274-a87c-786760c53db9.jpg?width=527&height=277", "/styles/anusara/71", " Anusara yoga is in the Tantric tradition, wanting to nurture the intrinsic goodness in all of us, and celebrate the difference and diversity of its practitioners. Classes revolve around a heart-oriented theme, using breath-coordinated movement and the 'Universal Principles of Alignment.' There are no set routines in Anusara, but it draws on hatha yoga to create a broad syllabus of 250 asana/poses."],
["Ashtanga/Mysore", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/405c2aff-f1be-416a-852e-1774aae4e707.jpg?width=527&height=277", "/styles/ashtanga/72", " Created by Sri T. Krishnamacharya and Sri K. Pattabhi Jois in 1948, the practice of Ashtanga ignites a cleansing, purifying and strengthening fire in the mind, body and nervous system. This is achieved by moving dynamically on the breath, engaging bandhas/locks and focusing the gaze. There are six series of asana/poses. The primary, beginner's series, is ordered as follows: sun salutations, standing poses, seated poses, inversions, backbends, relaxation. The Intermediate series follows the same order, but with more poses and variations. The four more advanced series are only appropriate for highly advanced students."],


["Aquanatal Yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/6f7d1e96-7a02-4a76-9d63-66e103c6d717.jpg?width=527&height=277", "/styles/aquanatal-yoga/152", "Aquanatal Yoga is a gentle yet most effective form of exercise for pregnant women and new mothers, allowing them to stretch without straining and overheating, and to access deep relaxation easily. Water provides a soothing yet stimulating environment to prepare for birth and most particularly for waterbirth. Aquanatal offers pregnant women more than just fitness: water facilitates relaxation and helps cultivate positive emotions. Birthlight Aquanatal Yoga is Accredited by Royal College of Midwives."],



["Baptiste Power Vinyasa Yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/70090898-9d4d-4c65-81a7-ce615988aad4.jpg?width=527&height=277", "/styles/baptiste-power-vinyasa-yoga/73", " Founded by Walt Baptiste in the 1940s, and evolved by his son Baron, Baptiste Power Vinyasa Yoga focuses on routines that empower practitioners to reach their full potential. Emphasis is placed on intense physical training, meditation and self inquiry to inspire confidence, passion and creativity."],
["Bhakti", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/fb28ea14-e11d-41ca-9779-1fd4bf67202a.jpg?width=527&height=277", "/styles/bhakti/124", "Bhakti is the yoga of love and devotion. There are, according to the Bhagavata Purana, nine forms of Bhakti; listening to scripture, praising (normally through song), focusing the mind on Vishnu, service, worship, paying homage, servitude, friendship, and surrender of the Self. The best known modern day exponent of Bhakti Yoga is Sri Mata Amritanandamayi, otherwise known as Amma, AKA the Hugging Mother. She has dedicated her life to serving the poor, the sick and the distressed, and hugs thousands of seekers a day."],
["Bihar/ Satyananda", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/0d356237-1351-4d85-a84d-b6e21097c0c7.jpg?width=527&height=277", "/styles/bihar/74", " Founded by Sri Swami Satyananda Saraswati in 1964, Bihar Yoga, also known as Satyananda, seeks to unite ancient yogic teachings and practices with modern science. Teachings consist of a systematic approach: asana for the physical body, breathing exercises for the energy body, and meditation to still the mind. It's a whole person, whole-life approach and encourages the practitioner to embrace Jnana, Bhakti and Karma Yoga as paths to spiritual growth."],
["Bikram", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/ef085c50-e90a-41f2-ad6f-e96bfa74cf17.jpg?width=527&height=277", "/styles/bikram/75", " Founded by Bikram Choudhury in the 1970s, Bikram Yoga is a set of 26 asana/poses conducted in a room heated to 105Â°F at 40% humidity. Each pose is performed twice in a 90-minute class. Although there are other 'Hot Yoga' methods, Bikram Yoga refers specifically to founder Bikram Choudhury's sequence of poses and accompanying breathing exercises."],
["Bodhiyoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/faba02e4-8d9d-4b79-bd1a-5900f49d1626.jpg?width=527&height=277", "/styles/bodhiyoga/145", " Bodhiyoga teaches mindfulness yoga, an approach that incorporates meditation, posture work and mindfulness skills. The roots of the Bodhiyoga school are firmly established in the Buddhist tradition, deriving inspiration and guidance from ancient and modern teachings on mindfulness. Bodhi comes from the root budh; to awake, become aware, know or understand. Mindfulness yoga is not just a technique but a way of being that involves presence of mind and responsiveness in our moment to moment lives."],
["Boxing yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/19e1d50a-c1da-4bb9-aec3-b31789a80ffb.jpg?width=527&height=277", "/styles/boxing-yoga/140", " BoxingYoga&#x2122; is a yoga-based training system to support fighters, athletes, and anyone with an active lifestyle; improving mental and physical strength and flexibility to prevent injury, maximise performance and maintain optimal health. Fusing boxing technique and philosophy with Ashtanga Yoga, BoxingYoga&#x2122; is an exciting and challenging yoga workout, originally born in a boxing club to give fighters competitive advantage. BoxingYoga&#x2122; is a new twist on an ancient discipline, merging innovative, boxing-inspired exercises and classical yoga postures together into one, hardcore, mind-body workout system."],
["Chair yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3799f55f-cc7d-40b4-9268-262b66aa1ac5.jpg?width=527&height=277", "/styles/chair-yoga/139", " If age, disability or injury prevents you from participating in a typical yoga class Chair Yoga could be a style to consider. Traditional asana/poses are adapted and supported with a chair making them accessible to everyone, and as the class will often be seated you wonâ€™t need to worry about getting up or down off the floor or standing for long periods. Even desk workers and travellers can take advantage of this approach, enabling all to experience the benefits of yoga regardless of physical limitations."],
["Core strength", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/273512ea-bee6-40eb-89e4-029dd0dfbeec.jpg?width=527&height=277", "/styles/core-strength/137", " Core Strength Yoga is a mix of traditional yoga asana and bespoke poses/twists. It aims to stretch and strengthen the back, hip, thigh and abdominal muscles. Sometimes the core is described as an energy centre. It's often a dynamic practice, moving on the breath in a vinyasa flow."],
["Critical alignment", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/f2e43020-aeff-4bba-bc9d-71216d91d66c.jpg?width=527&height=277", "/styles/critical-alignment/76", " Founded by Gert van Leeuwen in the 1980s, Critical Alignment (previously known as Bharata) relieves stress on the body and in the mind through precise alignment. It focuses on differentiating between the postural muscles and the deep movement muscles, situated closer to the bones, to create freedom and mobility in the spine. A specially designed headstand bench helps students achieve this."],
["Curvy ", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/75cbf35f-a8a8-43aa-b523-1133f518eafc.jpg?width=527&height=277", "/styles/curvy-yoga/141", " Founded by CEO (Curvy Executive Officer) Anna Guest-Jelley, a lifelong champion for womenâ€™s empowerment and body acceptance, Curvy Yoga empowers people of all shapes and sizes to embrace their current lives and bodies through yoga. Curvy Yoga is a movement. A full-figured, full-hearted, full-throttle frolic. A choose-your-own-yoga-adventure."],
["Desikachar/ Viniyoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/9662b25f-9318-49b5-a2fc-4e974ef4b9f8.jpg?width=527&height=277", "/styles/desikachar/77", " Son of Yoga legend Sri T. Krishnamacharya, T.K.V. Desikachar studied with his father for almost 30 years. His approach, sometimes called Desikachar Yoga, sometimes called Viniyoga, is based on evolving asana/poses, routines and techniques to match the needs of the student - to maximum therapeutic benefit."],
["Dharma Yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/dcd458f3-9c1c-433f-84b2-31e9fbf02934.jpg?width=527&height=277", "/styles/dharma/136", " Founded by Sri Dharma Mittra in 1975, Dharma Yoga Shiva Namaskar Vinyasa is a challenging vinyasa flow based on the traditions of raja yoga, but adapted for modern life. It's taught at a number of levels - Dharma I, II, III, IV and V - from beginners to advanced. The practice frees the flow of prana, moving it through the spine and into 'the physical, metabolic, intuitive, and bliss bodies,' promoting health and harmony."],
["Dru", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/25811371-8b77-400e-88ca-e9edbe781dbb.jpg?width=527&height=277", "/styles/dru/78", " Dru uses a combination of techniques including asana/poses, breathing exercises, hand gestures and affirmations in a dynamic and flowing style. Very much a therapeutic and stress-busting form of yoga, Dru practice concentrates on keeping the joints relaxed and flexible, with fluid movements originating from the spine. Energy Block Release sequences are used to release physical, mental and emotional tension through movement."],
["Face", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/bbcf567e-837a-4c32-a811-1cddda110836.jpg?width=527&height=277", "/styles/face-yoga/142", " Face Yoga is a natural way of looking and feeling younger and healthier. It combines face and neck exercises, massage, acupressure and relaxation to improve circulation and lymph flow, and remove toxins. It also improves skin tone, reducing dark circles around the eyes and puffiness. Practicised regularly it builds a holistic sense of wellbeing for mind, body and soul. Traditional Indian methods also includes eye exercises to improve eyesight and nose purifying to relieve headaches and build immunity."],
["Forrest", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/4bbfb834-ac40-4459-944b-e2c684fb2beb.jpg?width=527&height=277", "/styles/forrest/80", " Created by Ana Forrest, Forrest Yoga aims to help its practitioners with physical and emotional pain, using asana and breathing exercises that are intensely physical, internally focused, and compassionately taught. Forrest draws on elements of Native American culture to create a yoga style that is both physically beneficial and emotionally restorative."],
["Freedom Style Yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/ff214d5e-0bef-4290-9b05-81452274037e.jpg?width=527&height=277", "/styles/freedom-style-yoga/130", " Created by Erich Schiffmann, Freedom Style Yoga offers three practices: meditation, asana/poses, and 'the rest of the time.' Believing that each of us is an expression of all that is trustworthy - 'Life, Love, Truth, Presence, GOD' - practitioners tune in to their heart and conscience and trust in what they discover, bringing their innate creativity and wisdom to daily life."],
["Gentle", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/dc32aa77-7f65-45a8-9d18-ebd515f2fe6d.jpg?width=527&height=277", "/styles/gentle/131", " For people who want a slow practice that relaxes and restores. It's often recommended for people with injuries and chronic back problems. Great for older people, beginners and the more experienced practitioner who wants to counterbalance a more dynamic practice."],
["Hatha", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/9e7411ed-85f9-482b-8474-7f73431f7291.jpg?width=527&height=277", "/styles/hatha/84", " Although most modern schools of Yoga fall within the Hatha tradition, the term usually has a more specific meaning when applied to classes. A Hatha Yoga class will usually involve foundational asana/poses, gentle stretching, simple breathing exercises and seated meditation. A Hatha class is a good starting point for those wishing to explore Yoga, and works as a foundation for those who want to pursue other styles."],
["Himalayan Yoga Tradition", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/335b000d-a1e0-49f7-846a-f4211392ccaf.jpg?width=527&height=277", "/styles/himalayan-yoga-tradition/85", " Founded by Swami Rama, now led by his disciple Swami Veda Bharati, The Himalayan Yoga Tradition is a meditative approach, awakening students to their true nature. It is a systemic approach, drawing on a wide range of meditative traditions including Vipassana and Transcendental Meditation, teaching that they are interconnected. Students learn how to sit, how to breathe, how to use mantra and how to focus the mind."],
["Holistic", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/fc9e27b0-eabb-476f-9675-88f7471723c6.jpg?width=527&height=277", "/styles/holistic/86", " Holistic Yoga is a term that's sometimes used to mean whole body, and sometimes to denote a fusion of style or traditions, aiming to transform every aspect of the practitioner's life."],
["Hot", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3c2f54c5-f31f-4228-a5a1-c39819a79955.jpg?width=527&height=277", "/styles/hot/126", " Hot yoga studios are heated to between 95 and 105 degrees. The aim is to use the heat to get deeper into the poses, and flush out toxins. Classes can be fast or slow although generally they tend towards vinyasa flow style sequences. Hot options include Bikram, Moksha and Baptiste Power Yoga."],
["Integral", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/452dd7cb-c166-4f71-97cb-2f7fa0ca07c2.jpg?width=527&height=277", "/styles/integral/87", " Founded by Swami Satchidananda in 1966, Integral Yoga combines the various physical disciplines of Hatha Yoga, such as asana and breathing exercises, with six other branches: the meditation of Raja Yoga, the mantras of Japa Yoga, the selfless service of Karma Yoga, the intellectual approach of Jnana Yoga, and the devotional Bhakti Yoga. In classes, the physical elements are usually gentle and relaxing."],
["Integrated Yoga & Mindfulness", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/dc40d3dd-36f2-45d5-8ff4-9df4dc3ca1eb.jpg?width=527&height=277", "/styles/integrated-yoga-mindfulness/147", " Integrated Yoga & Mindfulness (IYM), teaches us how to work with our minds as much as our bodies, how to unlock flexibility and work with the stiffest muscle, the one between our ears. You will learn about first and second brain intelligence and how to listen and communicate using both â€“ in standalone mindfulness practices and IYM sequences. This approach develops a toolkit to self-reference in all actions and activities. When you shape mind as much as body ... all movement matters"],
["Integrative Yoga Therapy", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/2f16e8bf-3f2c-42b4-8b70-e18f3397a729.jpg?width=527&height=277", "/styles/integrative-yoga-therapy/88", " Founded by Joseph le Page in 1993, Integrative Yoga Therapy uses all elements of yoga to therapeutic effect for mind, body and spirit. Classes are usually taught one-to-one or in small groups to allow teachers to personalise the exercises and meditation to the student's needs."],
["ISHTA", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/ae60f0e8-a8a9-4809-912e-6c2cea538b3b.jpg?width=527&height=277", "/styles/ishta/89", " Created in the late sixties by Kavi Yogi Swarananda Mani Finger and his son Alan, ISHTA stands for Integrated Science of Hatha, Tantra and Ayurveda. This school wants its students to find balance through the combined physical practices of Hatha Yoga, the meditative and philosophical approaches of Tantra, and the body-awareness and healing properties of Ayurveda."],
["Iyengar", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3d89595c-13b1-42a3-87eb-627e9019a9e6.jpg?width=527&height=277", "/styles/iyengar/90", " Introduced to yoga by Sri T. Krishnamacharya in 1934, B.K.S. Iyengar went on to systematise over 200 classical yoga asana and 14 different Pranayama (breathing exercises). Iyengar Yoga focuses on alignment in asana/poses. The student must perform asana with precision, and hold them for lengthy periods to fully understand them. Iyengar classes can also involve props, to help students perfect asana. Pranayama is started only once a firm foundation in asana has been established."],
["Japa", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/4258c0cc-c50e-4158-a731-d1596a3220e6.jpg?width=527&height=277", "/styles/japa/98", " Japa Yoga practitioners repeat a mantra (a sound or word or group of words) to calm the mind, and as a path to transformation. Om is the seed mantra - described in the Upanishads (an ancient Hindu text) as the essence of Brahman â€“ the state of the highest reality, in which we exist only as awareness, at peace with ourselves, with all beings, and with the universe, in eternal bliss. Practitioners usually hold a mala - a string of 108 beads - in the right hand, repeating the mantra once for each bead."],
["Jivamukti", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/628341d0-12c3-4ef2-8106-0346eb9d005a.jpg?width=527&height=277", "/styles/jivamukti/91", " Created by David Life and Sharon Gannon in 1984, Jivamukti teaching is based on five principles designed to feed strong relationships with the practitioner and others. These principles are universal compassion including vegetarianism and animal rights, devotional practices which approach God/Self-realisation as the goal of yoga, the study of ancient scriptures, physical and mental health through 'deep listening', and meditation."],
["Jnana", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/82f6ec1a-c3c2-476f-a016-cfeb23858f80.jpg?width=527&height=277", "/styles/jnana/125", " Jnana is the yoga of Self-knowledge. In the Bhagavad Gita Krishna describes Jnana as understanding the difference between the body and the knower of the body - the soul. Ramana Maharishi is arguably the best-known recent times exponent of Jnana. He advocated a path of self-inquiry - asking oneself 'Who am I?' - as the path to Self-realisation. He taught that we are not the body - the senses, the cognitive organs, or the thinking mind; rather that we are Awareness - and that the nature of pure Awareness is existence-consciousness-bliss."],
["Kids", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/97d490b6-6a23-47d7-acbf-06df6faa6a24.jpg?width=527&height=277", "/styles/kids/93", " If Yoga's an important part of your life, you'll probably want your kids to feel the benefits too. Kids' Yoga classes normally use gentler asana/poses with an emphasis on the things they represent - such as animals - making it easier for kids to connect with the pose. Children are guided through relaxation and meditation exercises, helping them to centre and calm whilst remaining engaged."],
["Kirtan", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/6721e4f2-cfe2-4273-9d67-0a29c810096d.jpg?width=527&height=277", "/styles/kirtan/94", " Kirtan is the Sanskrit term for chanting, one of the traditional Indian yoga practices. Chanting is accompanied by classical Indian instruments such as the harmonium, while Sanskrit mantras are repeated using call and response, stilling the mind and opening the heart."],
["Kripalu", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/2744acb0-6185-41a1-bdf7-27fbc0aa0fe7.jpg?width=527&height=277", "/styles/kripalu/95", " Kripalu Yoga is a transformative school which wants to extend the benefits of the yoga class beyond the mat and into the life of its students. Kripalu classes use breathing exercises and asana/poses in a relatively gentle fashion, with the level of difficulty determined by the student."],
["Kriya", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/7d083dd5-0b30-4c8c-929a-9f890b30c340.jpg?width=527&height=277", "/styles/kriya/96", " Created by Paramahamsa Hariharananda in 1861, Kriya Yoga does not focus on the physical disciplines of Hatha Yoga. Rather it centres around Karma, Jnana and Bhakti Yoga, practices that aim to develop the student's spiritual standing through knowledge, selfless service, connection with the universe, and detachment from the physical world. These are goals which are achieved through careful synchronisation of breathing and thought."],
["Kundalini", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/7a6fa4e2-e1f2-4f65-bc45-46327d7965f3.jpg?width=527&height=277", "/styles/kundalini/97", " Kundalini Yoga was brought to the West by Yogi Bhajan in 1961. The kriyas (groups of exercises) of Kundalini Yoga are designed to release energy from the base of the spine, encouraging it to flow towards the head with the aim of elevating consciousness. The kriyas combine asana/poses with breathing exercises, hand gestures (mudras), body locks (Bandhas), chanting, mantra and meditation. Emphasis is placed on the synchronisation of breath and movement."],
["Meditation", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/af0baf6f-70d9-413c-b746-a7b97aa6886e.jpg?width=527&height=277", "/styles/meditation/127", " While asana/poses quiet the body, meditation quiets the mind. When both mind and body are quiet the practitioner can see themselves clearly, discovering their true nature, and ultimately, perhaps, experiencing existence as pure and boundless bliss. There are many different forms of meditation - from the secular to the religious. Each offers a different method - from the repetition of mantras, to the use of prayer beads."],
["Moksha", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3433ba45-e5fc-463c-87c0-88ca750205ef.jpg?width=527&height=277", "/styles/moksha/99", " Created by Ted Grand and Jessica Robertson, Moksha Yoga is a recently developed form of hot yoga, originating in Canada. Moksha uses a set of standing and floor poses, as well as intention setting, breathing exercises and relaxation techniques to help students stretch, strengthen, tone, calm and detox. Moksha has a strict code of environmental practice, ensuring that all of its affiliated studios produce heat with minimum eco impact."],
["Mudra", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/5287dec7-2971-4720-b9d6-a7ba4ab96cc6.jpg?width=527&height=277", "/styles/mudra/100", " Mudras are symbolic hand and finger gestures used to guide energy flow and rewire the brain into healing thought patterns."],
["Mum & Baby", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/d804d2f9-324b-45af-9712-69a7f0c9b2cc.jpg?width=527&height=277", "/styles/mum-and-baby/101", " Mum and baby classes use yoga to develop the bond between a mother and her new-born child - the yoga is usually served up with a big dollop of fun. Mothers do most of the work while the baby sits or lies on their Mum, or by her side. Sometimes the weight of the baby is used to develop Mum's strength and flexibility. Singing songs and baby massage can also feature."],
["Mum & Toddler", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/dff4b03f-2d5b-4f75-b125-ff361cdbb52a.jpg?width=527&height=277", "/styles/mum-and-toddler/102", " Mum and Toddler Yoga is designed to help build the bond between a mother and her little one, as well as encouraging the positive influences of yoga in the child's behaviour, and helping the mother with physical balance and stability."],
["Nidra", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/a5623771-f368-43a8-b08d-c0b4edddebe3.jpg?width=527&height=277", "/styles/nidra/104", " Yoga Nidra is sometimes described as 'yogic sleep.' Practitioners describe it as beyond the 'dreaming state' normally experienced during sleep. In the Nidra state you are simultaneously awake, and in a state of deep, undreaming relaxation. Besides relaxation, it's often used to promote healing, as well as spiritual insight and exploration."],
["Okido", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/42df85e7-cab7-49dc-a5b5-08a209072185.jpg?width=527&height=277", "/styles/okido/148", " Japanese master Masahiro Oki established his dojo at Mishima, Japan in 1967. Okido Yoga is based on the belief that true knowledge comes from an awakening of deep personal inner wisdom and that this can only be gained through individual experience of the natural laws of Change, Balance and Stability. Its practice addresses four aspects of human development - diet, breath, movement and mind-heart, whilst considering life holistically. It also emphasises the importance of learning to work with, and for, others as it is not possible to create a balanced life that does not include harmonious and caring interaction within society. Okido Yoga blends disciplines from Zen to martial arts and oriental medicine with traditional Indian yoga to form a uniquely fluid, dynamic and adaptable system."],
["Partner", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/71b15af2-1c61-49a5-8749-b049f368d0dd.jpg?width=527&height=277", "/styles/partner/105", " Partner Yoga classes consist of asana/poses designed for two people, to mutually beneficial effect. Asana are intended to stretch muscles using the weight and resistance of the other person's body. Partner Yoga can also be used to develop personal relationships and intimacy."],
["Phoenix Rising Yoga Therapy", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/7e8690b7-7212-4ee3-97ca-f64fbe4a19b4.jpg?width=527&height=277", "/styles/phoenix-rising-yoga-therapy/106", " Founded in 1985 by Michael Lee, Phoenix Rising Yoga Therapy is practiced within a safe, supportive environment encouraging exploration of the relationship between whatâ€™s happening in one's body and whatâ€™s happening in life. The non-directive approach of PRYT sessions provide opportunities to dialogue internally in a yoga class, and out loud in a yoga therapy one-on-one session. Body-centered awareness of this kind may bring about a release of physical or emotional tension leading to a better self-understanding and inner guidance that can be supportive in making life decisions."],
["Post Natal", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/d3ce050e-bace-4c22-870d-5c43402540fd.jpg?width=527&height=277", "/styles/post-natal/107", " Postnatal Yoga helps mothers regain their pre-pregnancy body, especially around the pelvic floor and abdomen. They also help promote patience, calm and relaxation in what can be a stressful time. Postnatal classes also have a great deal of crossover with mum and baby classes, encouraging bonding between parent and child."],
["Power Yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/79c00f9e-9dfa-4000-8adb-89161abdb3e4.jpg?width=527&height=277", "/styles/power/108", " Bryan Kest and Beryl Bender Birch Power both use the term 'Power Yoga' to describe their approach - a pulse raising practice based on Ashtanga, with the same focus on developing strength and flexibility. Others have followed, also using the term to describe a dynamic practice that's more varied in content than Ashtanga, and that doesn't follow any set routine."],
["Prana Flow", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/14b858ee-96f8-4504-b888-efbb7b3ad8b8.jpg?width=527&height=277", "/styles/prana-flow-yoga/128", " Created by Shiva Rea, Prana Flow is an energetic flowing yoga. The aim is a direct experience of prana, or life-energy. It uses traditional and modern approaches drawn from Krishnamacharya, Tantra, Ayurveda, Bhakti, Somatics, and from Shiva's own experience."],
["Pranayama", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/090bcc34-e098-4edd-8f86-0968ce010145.jpg?width=527&height=277", "/styles/pranayama/109", " Pranayama means 'extension of the life force' and offers a range of controlled breathing techniques which help calm, connect and rebalance body and mind. It can be used within an asana driven practice as a way of deepening poses and energising the body, and as a stand-alone practice to create calm. It is especially useful in dealing with stress and anxiety."],
["Pre-natal", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/d2a6b3f9-4ac5-48f4-b84d-70eb83fe15b8.jpg?width=527&height=277", "/styles/pre-natal/110", " There are many different approaches to practicing yoga during pregnancy. Most teachers will focus on relaxation and breathing techniques, but there are classes which include asana to aid the healthy development of the baby, and prepare the mother for the birth."],
["Raja", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/961ece6a-8a75-407e-800f-df433b254e1f.jpg?width=527&height=277", "/styles/raja/111", " There are eight limbs of yoga in the traditional scriptures of yoga. The first four are the basis of most modern 'Hatha Yoga' practices, which centre around the physical. Raja Yoga is more concerned with the mind and spirit and relates to the four higher practices - Pratyahara (withdrawal of the senses), Dharana (concentration of mind), Dhyana (meditation) and Samadhi (enlightenment or universal consciousness)."],
["Restorative", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/e2937c14-558e-49c8-88cb-4854d3a511b5.jpg?width=527&height=277", "/styles/restorative/112", " Restorative Yoga helps us relax in order to relieve stress, calm the mind and allow the body to release accumulated tensions. Props are used to help students relax completely into a pose. A typical class will work the spine in every direction, both soothing and stimulating internal organs, leaving the practitioner feeling re-energised."],
["Scaravelli", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/842222be-addf-4bc2-a73c-0c73130501c6.jpg?width=527&height=277", "/styles/scaravelli/113", " Vanda Scaravelli studied with T.K.V. Desikachar and B.K.S Iyengar, and developed her style of yoga in response to these teachings. Rooted in the belief that yoga should be used to bring freedom to the body, not to control it, Scaravelli Yoga uses the breath, gravity and the body's natural tendencies to help students arrive at poses without stress or strain."],
["Seniors", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/3807cf79-de4c-4920-807c-0ead02acfdcb.jpg?width=527&height=277", "/styles/seniors/135", " Often targeted at the 50 or 60+, Seniors' Yoga is usually a slower and more relaxing practice that improves flexibility and circulation, mood, sleep patterns and breathing habits. Practiced regularly it can help to reduce everyday aches and pains, and alleviate high blood pressure, and the symptoms of osteoporosis, rheumatism and arthritis."],
["Shadow", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/080dabc2-7248-4a8e-b2ca-510ac8becd24.jpg?width=527&height=277", "/styles/shadow/114", " Created by Natanaga Zhander, also known as Shandor Remete, Shadow Yoga uses three fluid routines, or 'preludes', to undo habitual patterns of tension which obstruct the movement of 'the vital breath' and energy. The aim of these preludes is to increase the flow of energy, and to facilitate the practice of other asana/poses. In practice, Shadow Yoga resembles Tai Chi, but its movements are more directed and specific to certain parts of the body and flows of energy."],
["Sivananda", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/e2041370-3f09-4340-aed9-f5d5d8c16ccf.jpg?width=527&height=277", "/styles/sivananda/115", " Created by Swami Sivananda and systematised by Swami Vishnudevananda, Sivananda Yoga is a whole-life approach based on five principles: asana to increase circulation and flexibility; breathing exercises to release energy into the body; full relaxation of the body and mind; a balanced, vegetarian diet, positive thinking and meditation. Sivananda classes involve a mixture of breathing exercises and poses, centering around 12 asana/poses. Teachers will adjust the difficulty of these asana according to the ability of the class."],
["Special yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/89f6558b-dd94-481e-a16a-a411808bf5a8.jpg?width=527&height=277", "/styles/special-yoga/146", " Special Yoga is a therapeutic practice for children with special needs. The aim is to help them find a relaxed, peaceful place within in order to reach their fullest potential. The practice is adapted to suit the individual child with great compassion and care. Special Yoga uses asana, pranayama, massage, sound, and deep relaxation. All children are welcome, all children are perfect, all children are special."],
["Stand up paddleboard yoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/34d4fd83-ca58-4b1b-a35e-2a313a87e0fc.jpg?width=527&height=277", "/styles/stand-up-paddleboard-yoga/149", " Stand Up Paddleboard Yoga has grown rapidly in popularity over the past decade. As it's performed on a paddleboard, it requires awareness of weight transfer and balance, which builds stability, flexibility, and core strength. The SUP Yoga teaching focus is also on alignment and the dristi/focal point. (Teachers use the horizon a lot as this helps balance, gazing up is harder!) Although generally practiced on gentle waters - a calm sea, a lake, in a swimming pool, for a greater challenge SUP Yoga can be practiced on choppier waters, and for a more stable introduction, a sandy beach will do just fine. The bigger the SUP board the more stable it is, ideally at least 10 feet long and 30 inches wide. As this activity involves a lot of falling in, particularly for beginners, make sure you're a confident swimmer. Most poses can be done on a SUP but as the board is moving there is more risk, especially in poses like headstand, so these options are reserved for very experienced students. Meanwhile nobody feels like they are missing out as there is loads of fun to be had in the simpler poses."],
["Sun Power", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/94870629-b109-4e6c-a6b6-1e865bdc058d.jpg?width=527&height=277", "/styles/sun-power-yoga/143", " Founded by Anne-Marie Newland in 2001, Sun Power Yoga is an eclectic mix of Dynamic Hatha for alignment, Sivananda for subtle mind and breath-work, and Astanga Vinyasa Yoga for strength and flexibility."],
["Svaroopa", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/9ff31827-4bf9-4846-b1c2-9671f530134f.jpg?width=527&height=277", "/styles/svaroopa/116", " Created by Swami Nirmalananda Saraswati, Svaroopa - meaning 'self-knowledge' - uses a technique called 'Core Opening' to decompress the spine, lifting pressure off the nervous system, organs and muscles so that energy can flow without blockages. Starting at the base of the back, Svaroopa Yoga works on releasing the tension in the muscles around the spine, in order to allow more fluid movement and better relaxation throughout the body."],
["Svastha", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/e15a8540-f241-4b70-af05-537c75e053e7.jpg?width=527&height=277", "/styles/svastha-yoga/129", " Created by A.G. and Indra Mohan, Svastha is a Sanskrit word meaning 'complete health and balance.' Svastha Yoga combines Raja Yoga with Ayurveda and is rooted in the teachings of Sri T. Krishnamacharya â€“ yoga master, scholar, and healer. This integrated approach, combining a physical practice with dietary and lifestyle recommendations, is personalised to the needs of the individual."],
["Tantra", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/07aa74a2-122d-4a8f-a1a3-d4c0f716ee28.jpg?width=527&height=277", "/styles/tantra/117", " Although Tantra is frequently associated with sexual practice in the Western world, its roots in Indian culture are far more complex and all-encompassing - maintaining that the world around us is a joyful expression of the Divine. In Tantra, asana/poses, breathing exercises and hand gestures are important, but the focus on creating a human connection with the Divine is the underlying aim."],
["Teens", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/c26b63c1-146c-4c9c-a7df-ba0ada79634d.jpg?width=527&height=277", "/styles/teens/118", " Given the barrage of changes teens go through, there are obvious benefits to practicing yoga. Asana/poses will help create stability and rootedness, and build the habits of good posture, while breathing and meditation techniques can help teens deal with the emotions of growing up."],
["Tibetan", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/5b1995bf-631f-459b-bfdd-722abc73e7a9.jpg?width=527&height=277", "/styles/tibetan/119", " Tibetan Yoga comprises five yoga exercises called The Five Rites, plus breathing exercises, meditation and positive thinking. They're based on the practices of Tibetan monks who, it's claimed, lived long, active lives and stayed 'forever young.' The entire routine, which can be practiced in less than twenty minutes, aims to promote the free flow of energy around the body, to balance the chakras and hormones, and regulate the digestive, cardiovascular, nervous and respiratory systems."],
["Trauma Sensitive", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/323fb73e-813f-408a-8036-e6b3198f44c9.jpg?width=527&height=277", "/styles/trauma-sensitive-yoga/150", " Anything from sexual abuse to military combat can trigger trauma, causing nightmares and flashbacks, isolation and irritation, which further elevate stress levels. The brainâ€™s preoccupation with fear and self-preservation means staying in the present feels like an impossible challenge. The sufferer gets stuck in the past, unable to feel or respond to current circumstances, or move forwards. By focusing on the breath and slow movements, Trauma Sensitive Yoga gives the sufferer a safe space in which to feel comfortable and grow strong. Working on the fight or flight sympathetic nervous."],
["Triyoga", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/dded0fed-f988-4e3b-9875-e805b8103300.jpg?width=527&height=277", "/styles/triyoga-/92", " TriYoga&#x00AE; uses a Kundalini inspired sequenced flow of asana/poses, hand gestures and breathing to increase energy and mental clarity. It teaches a student to use movement as a relaxation method, radiate wave-like movements from their spine and use only the necessary muscles for each pose or transition. Classes use one of seven routines, which become more challenging as the student's skill deepens."],
["Vinyasa Flow", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/18fb76fa-5081-4ee4-8c90-5b8c39a32bc6.jpg?width=527&height=277", "/styles/vinyasa-flow/120", " Vinyasa means 'breath-synchronised movement.' In a Vinyasa Flow class, expect to move from one pose to another on an inhale or exhale."],
["Windfire", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/c74f384e-ea0b-4479-8b99-a167992c71d3.jpg?width=527&height=277", "/styles/windfire/79", " Created in 1992 by Godfrey Devereux, now known as Godfridev, Windfire Yoga is a systematic, somatic exploration of the relationship between body, mind and consciousness as expressions of a single spectrum of intelligence. Relying as it does on the inherent intelligence of the body, rather than flexibility, skill or strength, this training method allows anyone to enjoy a seamless transition from separateness to integration."],
["Yin ", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/de08427d-a5b0-475c-a7fb-032898afc450.jpg?width=527&height=277", "/styles/yin-yoga/121", "Created by Paulie Zink, Yin Yoga aims to restore our innate ability to move with fluidity, power and grace. It uses long, passive holds to exercise all soft tissues, clear energetic blockages and promote circulation. The longer holds, and the emphasis on letting go, encourage profound and meditative exploration. Yin Yoga improves flexibility at any age, but perhaps especially into old age, when function declines.Advanced Yin yoga is based upon Daoist energy alchemy and uses animal postures and primal movement exercises to develop the invigorating five transforming elements of Earth, Metal, Water, Wood, and Fire that are contained in the universal life force. The rhythm of yin and yang is harmonized and enhanced with powerful flowing sequences that temper the bones and build core strength and balance."],
["Yin Yang", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/de08427d-a5b0-475c-a7fb-032898afc450.jpg?width=527&height=277", "/styles/yin-yoga/121", "Created by Paulie Zink, Yin Yoga aims to restore our innate ability to move with fluidity, power and grace. It uses long, passive holds to exercise all soft tissues, clear energetic blockages and promote circulation. The longer holds, and the emphasis on letting go, encourage profound and meditative exploration. Yin Yoga improves flexibility at any age, but perhaps especially into old age, when function declines.Advanced Yin yoga is based upon Daoist energy alchemy and uses animal postures and primal movement exercises to develop the invigorating five transforming elements of Earth, Metal, Water, Wood, and Fire that are contained in the universal life force. The rhythm of yin and yang is harmonized and enhanced with powerful flowing sequences that temper the bones and build core strength and balance."],
["Yoga & Pilates", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/fb627cc9-d078-4ef1-8a98-0aa8c22b1e2e.jpg?width=527&height=277", "/styles/yoga-and-pilates/81", " Yoga and Pilates fusion classes, sometimes known as Yogilates, bring together techniques and practices from the two practices. Pilates, a physical exercise system created by Joseph Pilates in Germany, is centered on deliberate control of the muscles from a strong core. A Yogilates class will use this controlled approach combined with asana/poses from the yogic tradition, as well as using breathing and relaxation techniques."],
["Yoga & Qigong", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/4002b4cd-c17b-4637-97f9-93953b22a37d.jpg?width=527&height=277", "/styles/yoga-and-qigong/82", " The Chinese practice of Qigong has many similarities with yoga, so combinations of the two traditions have become popular. Both use controlled breathing, movement from a strong core and meditation to achieve physical and mental balance."],
["Yoga for sports", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/e0b6b7e6-2a90-4701-a96a-a9bf3efd426d.jpg?width=527&height=277", "/styles/yoga-for-sports/123", " Yoga for Sports will improve performance on the slopes, track etc. by building structural stability, range of movement, aerobic capacity, endurance and alignment - while minimising the risk of injury."],
["Yoga raves", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/de213c2a-e25b-4c49-abb1-95a73a1218d0.jpg?width=527&height=277", "/styles/yoga-raves/144", " Rave Yoga blends music and yoga to create a supercharged party atmosphere. Whether itâ€™s cheesy hits, the Top 40 or something more mellow, you'll be sharing your love of music with fellow yogis. Most classes practice vinyasa flow, while some also offer chanting and meditating. UV lighting is a must, and mocktails are often served afterwards Â­â€“ leaving you energised, not hungover."],
["Yoga therapy", "http://cdn.yogaclicks.com/images/yogaimages/Style.Image/9501e240-5fa2-4dca-ba50-28690283ac3e.jpg?width=527&height=277", "/styles/yoga-therapy/122", " Yoga Therapy treats people with health issues. Research provides evidence that it can help alleviate many ailments, including back pain, hypertension, heart conditions, asthma, diabetes and stress. It uses gentle exercises, meditation, relaxation and breathing techniques to help restore physical and emotional equilibrium. Tuition is offered one to one, and in classes targeting specific health challenges such as back pain, cancer, depression and MS."]];


var answers = new Array(answerpoints.length);
for (var i = answerpoints.length - 1; i >= 0; i--) answers[i] = -1;


var topicprogress = new Array(topics.length);
for (var i = topicprogress.length - 1; i >= 0; i--) topicprogress[i] = -1;


//alert(" Questions length : " + questions.length)
//alert("Question topics length : " + questiontopics.length)
//alert("Topics length : " + topics.length)
//alert("Styles length : " + styles.length)


var quizanswer;
var subquestionstage = 0;
var topicstage = 0;
var complete = 0;
var zzz = 0;


function quizzywiz(quizanswer) {
    if (quizanswer == true) {
        //alert("You pressed true")

        if (topicprogress[topicstage] == -1) {//If the question asked is a topic question
            //alert("The topic question true")
            
            topicprogress[topicstage] = 1;
            $("#questiontopic").html(topics[topicstage]);
            $(".questionsub").show(100);
            $("#questionsub").html(questions[subquestionstage]);
            $("#questionnum").text("Q" + (topicstage + 1));
            $("#questionsubnum").text(questiontopicsletters[subquestionstage].toUpperCase());
        }
        else if (topicprogress[topicstage] == 1) {//Only runs if topic has been selected
           
            //alert(questions[subquestionstage])
            answers[subquestionstage] = 0;
            subquestionstage++;
            $(".questionsub").show(100);
            $("#questionsub").html(questions[subquestionstage]);
            $("#questionsubnum").text(questiontopicsletters[subquestionstage].toUpperCase());
            //            $("#questionsubnum").text(topicstage +""+)
            if (subquestionstage == questions.length) {
                finale();
            }
            if (questiontopics[subquestionstage] > topicstage) {//Checks to see if this topic has been completed
                //alert("You finally got this place")
                topicstage++;
                $("#questionnum").text("Q" + (topicstage + 1));
                $("#quizimage").attr('src', topicimages[topicstage]);
                if (questiontopics[subquestionstage] == topicstage) {
                    $("#questiontopic").html(topics[topicstage]);
                    $(".questionsub").hide(100);
                }
            }
        }
    }
    else if (quizanswer == false) {

        //alert("You pressed false")
        if (topicprogress[topicstage] == -1) {//If the question asked is a topic question
            //alert(topics[topicstage])
          
            //alert("The topic question false")
            topicprogress[topicstage] = 1;
            topicstage++;
            $("#questionnum").text("Q" + (topicstage + 1));
            $("#quizimage").attr('src', topicimages[topicstage]);
            $("#questiontopic").html(topics[topicstage]);
            $(".questionsub").hide(100);
            $("#questionsub").html(topics[topicstage]);
            while (questiontopics[subquestionstage] < topicstage) {
                //alert("Subquestion stage is : " + subquestionstage)
                subquestionstage++;
            }
            if (subquestionstage == questions.length) {
                finale();
            }
        }
        else if (topicprogress[topicstage] == 1) {
            //alert("finally got here")
            //alert(questions[subquestionstage])
            
            answers[subquestionstage] = 1;
            subquestionstage++;
            $("#questionsub").html(questions[subquestionstage]);
            $(".questionsub").show(100);
            $("#questionsubnum").text(questiontopicsletters[subquestionstage].toUpperCase());
            if (subquestionstage == questions.length) {
                finale();
            }
            else if (questiontopics[subquestionstage] > topicstage) {//Checks to see if this topic has been completed
                //alert("Works to this place")


                topicstage++;
                $("#questionnum").text("Q" + (topicstage + 1));
                $("#quizimage").attr('src', topicimages[topicstage]);
                if (questiontopics[subquestionstage] == topicstage) {
                    $("#questiontopic").html(topics[topicstage]);
                    $(".questionsub").hide(100);
                }

            }
        }

    }
  

    if (subquestionstage !== 0) {
        $(".backbutton").show();
    } else {
        $(".backbutton").hide();
    }





    updateprogress();

}
var progressbar = 0; progressvar = 0;

function updateprogress() {

    progressbar = Math.floor((subquestionstage / questions.length) * 100); //Percentageprogress

    $(".progress-bar").css({ "width": progressbar + "%" });



}

function backButton() {
    //Go back previous question
    //If you're on topic 0 subquestion 0, do nothing
    //alert("Topic stage is: "+topicstage+" and subquestionstage is: "+subquestionstage)
    if (topicstage == 0 && subquestionstage == 0) {
        $(".questionsub").hide(100);
        topicprogress[0] = -1;
        $(".backbutton").hide();
        zzz = 0;
        //initialfill();
    }
        //If on a subquestion which isn't the first in it's topic, go back one sub question
    else if (questiontopics[subquestionstage - 1] == topicstage) {
        answers[subquestionstage] = -1;
        subquestionstage--;
        $("#questionsub").html(questions[subquestionstage]);
        $("#questionsubnum").text(questiontopicsletters[subquestionstage].toUpperCase());
    }
        //Else go back one topic
    else {
        topicstage--;
        topicprogress[topicstage] = -1;
        while (questiontopics[subquestionstage - 1] >= topicstage) {
            answers[subquestionstage] = -1;
            subquestionstage--;
        }
        $("#quizimage").attr('src', topicimages[topicstage]);
        $("#questionnum").text("Q" + (topicstage + 1));
        $("#questiontopic").html(topics[topicstage]);
        //$(".questionsub").hide(100);
        if (topicstage == 0) {
            //$(".backbutton").hide();
            topicprogress[0] = -1;
            subquestionstage = 0;
        }
    }


    progressbar = Math.floor((subquestionstage / questions.length) * 100); //Percentageprogress

    $(".progress-bar").css({ "width": progressbar + "%" });
    //alert("Topic stage is now: " + topicstage + " and subquestionstage is: " + subquestionstage)
}


var letters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k'];
var numselections = 10;
var histyle;
var hinum;
var Uniquescores;
var statey;
var stylepoints = [-1, -1, 1, 1, 1, 1, 1, 1, 1, 1, -1, -1, -1, 1, -100, -1, 1, 1, -1, 1, 1, -1, 1, 1, -1, 1, 1, 1, 1, 1, 1, 1, 1, 1, -100, 1, 1, 1, 1, 1, 1, 1, -100, -100, 1, 1, -100, 1, -100, 1, 1, 1, -100, 1, 1, 1, -100, 1, 1, -100, 1, 1, 1, 1, 1, -100, 1, 0, 1, 1, 1, 1, 1, -1, -1, -1, -1, -1];
var n = 0;
function finale() {
    //alert("Finale")

    //var width = $(window).width();
    //if (width > 992) {
    //    $(".progresstext").css({ "top": "6.5em" });
    //}
    //else {
    //    $(".progresstext").css({ "top": "8.5em" });
    //}
    //$(".progresstext").html("Match Complete!");
    //$(".progresstext").css({ "font-size": "2.2em" });
    $(".progress-bar").css({ "width": "100%" });
    $(".progress-bar").removeClass("active");
    for (i = 0; i < answerpoints.length; i++) {
        for (j = 0; j < stylepoints.length; j++) {
            if (answers[i] == 0) {//If answer is true
                stylepoints[j] += answerpoints[i][(((j + 1) * 2) - 1)];
                //n++;
            }
            else if (answers[i] == 1) {//If answer is false
                stylepoints[j] += answerpoints[i][(((j + 1) * 2))];
                //n++;
            }
        }
    }





    var stylesort = new Array(stylepoints.length);
    for (var i = 0; i < stylepoints.length; i++) stylesort[i] = stylepoints[i];//duplicates the stylepoints array, calling st stylesort

    stylesort.sort(function (a, b) { return b - a });//Sorts stylesort[] into order, highest to lowest.



     hinum = new Array(numselections);
    for (var i = numselections - 1; i >= 0; i--) hinum[i] = 0;

     histyle = new Array(numselections);
    for (var i = numselections - 1; i >= 0; i--) histyle[i] = 0;


   
    var sparevar = 0;

    while (sparevar < numselections - 1) {//While the top styles are being decided
        for (k = 0; k < stylepoints.length; k++) {
            if (stylepoints[k] == stylesort[sparevar]) {
                hinum[sparevar] = stylepoints[k];
                histyle[sparevar] = k;
                sparevar++;
            }
        }
    }
    sparevar = 0;
    sparevar2 = 0;

    while (sparevar < numselections - 1) {
        if (hinum[sparevar] == hinum[sparevar + 1]) {//50% chance to swap style with the next style, if they're the same value
            if (Math.random() > 0.5) {
                sparevar2 = histyle[sparevar];
                histyle[sparevar] = histyle[sparevar + 1];
                histyle[sparevar + 1] = sparevar2;
            }
        }
        sparevar++;
    }
    for (x = 0; x < 11; x++) {
        console.warn("Style " + styles[histyle[x]] +  " has :" + hinum[x] + " points");
    }
  







    var count = 0; counter = 0;
    var countarray = {};


    if (hinum[0] == 1) {//Select a random style with the score of one
        for (a = 0; a < styles.length; a++) {
            if (stylepoints[a] == 1) {
                countarray[count] = a;
                count++;
            }
        }
        histyle[0] = countarray[(Math.floor(Math.random() * count))];
        histyle[1] = countarray[(Math.floor(Math.random() * count))];
        histyle[2] = countarray[(Math.floor(Math.random() * count))];
        histyle[3] = countarray[(Math.floor(Math.random() * count))];
        histyle[4] = countarray[(Math.floor(Math.random() * count))];
    }

    //for (i = 0; i < styles.length; i++) {
    //    console.warn("Style number " + i + " is: " + styles[i] + " Style scored :" + stylepoints[i] + " points")
   // stylesort.sort(function (a, b) { return b - a });//Sorts stylesort[] into order, highest to lowest.
    //}

  //  console.warn(numselections[]);
    for (var i = 0; i < answers.length; i++) {
    //    if (numselections[i] < 20) {

    //        numselections = numselections.indexOf(numselections[i]);
        //        alert(numselections);
        console.warn("Q" + i + " : " + answers[i]);
       }
    //}


    //hinum = hinum.filter(function (x) {
    //    return x > 30;
    //});
    //numselections = hinum.length;







    $(".quiz").hide();



    $("#rightimage").attr('src', stylescomplete[histyle[numselections -1]][1]);
    $("#stylename").text(stylescomplete[histyle[0]][0]);
    $("#finalimage").attr('src', stylescomplete[histyle[0]][1]);
    $("#finaltext").html(stylescomplete[histyle[0]][3]);
    $("#finallink").attr("href", stylescomplete[histyle[0]][2]);
    $("#leftimage").attr('src', stylescomplete[histyle[1]][1]);
    $(".final").show();


     statey = hinum[0];
      Uniquescores = hinum.unique();

    resizeDiv(true);



}


Array.min = function (array) {
    return Math.min.apply(Math, array);
};
Array.max = function (array) {
    return Math.max.apply(Math, array);
};
Array.prototype.contains = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === v) return true;
    }
    return false;
};
Array.prototype.unique = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.contains(this[i])) {
            arr.push(this[i]);
        }
    }
    return arr;
};
var place = 1;
var aaa = numselections -1;var bbb = 0;var ccc = 1;
function scrollquiz(direction) {
    //console.warn(aaa + "-" + bbb + "-" + ccc);
    if (direction == 'right') {
        if (aaa == numselections - 1) { aaa = 0 }
        else (aaa++);
        if (bbb == numselections - 1) { bbb = 0 }
        else (bbb++);
        if (ccc == numselections - 1) { ccc = 0 }
        else (ccc++);
    }
    else if (direction == 'left') {
        if (aaa == 0) { aaa = numselections - 1 }
        else (aaa--);
        if (bbb == 0) { bbb = numselections - 1 }
        else (bbb--);
        if (ccc == 0) { ccc = numselections - 1 }
        else (ccc--);
    }

    //console.warn(aaa + "-" + bbb + "-" + ccc);
    //console.warn(stylescomplete[histyle[aaa]][0] + "-" + stylescomplete[histyle[bbb]][0] + "-" + stylescomplete[histyle[ccc]][0]);
    //console.warn("----------------------------");
  




    if (statey === hinum[bbb]) {
        


        $(".ribbon-green").html(ordinal_suffix_of(place) + " tied");
    } else if (statey < hinum[bbb]) {
        //bigger
        if (bbb == 0) {
            place = Uniquescores.indexOf(Array.max(Uniquescores) ) + 1;
        } else (place--);



        $(".ribbon-green").html(ordinal_suffix_of(place) + " match");
     
    } else if (statey > hinum[bbb]) {
        //smaller
        if (bbb == numselections - 1) {
            place = Uniquescores.indexOf( Array.min(Uniquescores) ) + 1;
          
        }
        else (place++

            );



        $(".ribbon-green").html(ordinal_suffix_of(place) + " match");
    }


    statey = hinum[bbb];



    $("#rightimage").attr('src', stylescomplete[histyle[aaa ]][1]);

    $("#stylename").text(stylescomplete[histyle[bbb]][0]);
    $("#finalimage").attr('src', stylescomplete[histyle[bbb]][1]);
    $("#finaltext").html(stylescomplete[histyle[bbb]][3]);
    $("#finallink").attr("href", stylescomplete[histyle[bbb]][2]);


    $("#leftimage").attr('src', stylescomplete[histyle[ccc]][1]);



    resizeDiv(true);

}

function initialfill() {
    $("#questiontopic").html(topics[topicstage]);
    $(".questionsub").hide(100);
    $("#quizimage").attr('src', topicimages[0]);
}


function ordinal_suffix_of(i) {
    var j = i % 10,
        k = i % 100;
    if (j == 1 && k != 11) {
        return i + "<sup>st</sup>";
    }
    if (j == 2 && k != 12) {
        return i + "<sup>nd</sup>";
    }
    if (j == 3 && k != 13) {
        return i + "<sup>rd</sup>";
    }
    return i + "<sup>th</sup>";
}

$(document).ready(function () {
    initialfill();

});