# TCFConsult

Application MAUI de collecte de documents comptables, conçue pour évoluer vers une plateforme SaaS multi-cabinets.

## Premier incrément

- Importer des pages photographiées ou scannées depuis le sélecteur Android.
- Générer un PDF localement.
- Conserver les PDF dans l’espace privé de l’application.
- Préparer le dépôt vers Google Drive via une API sécurisée.

## Architecture

- `src/TCFConsult.Domain` : entités et règles métier.
- `src/TCFConsult.Application` : cas d’usage et ports.
- `src/TCFConsult.Infrastructure` : adaptateurs de stockage et PDF.
- `src/TCFConsult.Mobile` : interface .NET MAUI et navigation.

Pour le SaaS, chaque cabinet connectera son propre Drive. OAuth sera géré côté serveur, avec stockage isolé par tenant et contrôle d’accès côté API. Aucun jeton OAuth ne sera intégré à l’APK.

## Build Android

Chaque push sur `main` lance la compilation et publie l’APK Android comme artefact GitHub Actions sous le nom `TCFConsult-Android-APK` (conservation 14 jours).

## Limites actuelles

Le flux actuel importe des images produites par l’appareil photo ou une autre application. Le scan guidé avec détection des bords et recadrage automatique viendra dans un incrément suivant. L’intégration Google Drive n’est pas encore branchée.