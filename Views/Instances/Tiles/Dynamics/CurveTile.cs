using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Interfaces.Tiles;
using ScriptableObjects;
using UnityEngine;

namespace Views.Instances.Tiles.Dynamics
{
    public class CurveTile : TileBase, IMovableTile, IRenderableTile, IRotatableTile, IThrowableTile
    {
        [SerializeField] private Transform transformRotatable;

        [SerializeField] private MeshRenderer meshRendererBase;
        [SerializeField] private MeshRenderer meshRendererLine;

        [SerializeField] private ScriptableObjectElectricMaterials electricMaterials;

        private RotateStatus _rotateStatus;
        private ElectricStatus _electricStatusLine;

        private Sequence _sequenceRotate;
        private Coroutine _coroutineThrow;

        private float _currentRotationY;

        public void SetRotateStatus(RotateStatus rotateStatus)
        {
            _rotateStatus = rotateStatus;
        }

        public void SetElectricStatusLine(ElectricStatus electricStatus)
        {
            _electricStatusLine = electricStatus;
        }

        public void Take()
        {
            meshRendererBase.enabled = true;
            meshRendererLine.enabled = true;
        }

        public void Release()
        {
            meshRendererBase.enabled = false;
            meshRendererLine.enabled = false;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }

        public void Render()
        {
            meshRendererLine.material = electricMaterials.GetElectricMaterial(_electricStatusLine);
        }

        public void RenderIlluminate(LineDirection lineDirectionIn, LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLine.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLine.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLine.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLine.material = electricMaterials.GetIlluminatedMaterial(_electricStatusLine);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void RenderDarken(IEnumerable<ElectricStatus> electricStatuses)
        {
            var hashSetElectricStatuses = new HashSet<ElectricStatus>(electricStatuses);

            if (hashSetElectricStatuses.Contains(_electricStatusLine))
            {
                meshRendererLine.material = electricMaterials.None;
            }
        }

        public void RenderShort(ShortedStatus shortedStatus, LineDirection lineDirectionIn,
            LineDirection lineDirectionOut)
        {
            switch (_rotateStatus)
            {
                case RotateStatus.Rotate0:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Down:
                        case LineDirection.Left:
                        {
                            meshRendererLine.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate90:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Left:
                        {
                            meshRendererLine.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate180:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Up:
                        case LineDirection.Right:
                        {
                            meshRendererLine.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
                case RotateStatus.Rotate270:
                {
                    switch (lineDirectionIn)
                    {
                        case LineDirection.Right:
                        case LineDirection.Down:
                        {
                            meshRendererLine.material = electricMaterials.GetShortedMaterial(shortedStatus);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void RenderReset()
        {
            meshRendererLine.material = electricMaterials.None;
        }

        public void Rotate()
        {
            _sequenceRotate?.Kill();

            var targetRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            while (_currentRotationY > targetRotationY)
            {
                targetRotationY += 360.0f;
            }

            _sequenceRotate = DOTween.Sequence()
                .Append(DOTween
                    .To(() => _currentRotationY, value => { _currentRotationY = value; }, targetRotationY, 0.2f)
                    .SetEase(Ease.OutCubic))
                .OnUpdate(() => { transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f); })
                .OnComplete(() =>
                {
                    _currentRotationY = _rotateStatus switch
                    {
                        RotateStatus.Rotate0 => 0.0f,
                        RotateStatus.Rotate90 => 90.0f,
                        RotateStatus.Rotate180 => 180.0f,
                        RotateStatus.Rotate270 => 270.0f,
                        _ => throw new Exception()
                    };

                    transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
                }).SetLink(gameObject);
        }

        public void RotateImmediate()
        {
            _sequenceRotate?.Kill();

            _currentRotationY = _rotateStatus switch
            {
                RotateStatus.Rotate0 => 0.0f,
                RotateStatus.Rotate90 => 90.0f,
                RotateStatus.Rotate180 => 180.0f,
                RotateStatus.Rotate270 => 270.0f,
                _ => throw new Exception()
            };

            transformRotatable.localEulerAngles = new Vector3(0.0f, _currentRotationY, 0.0f);
        }

        public void RotateReset()
        {
            _sequenceRotate?.Kill();
            _rotateStatus = RotateStatus.Rotate0;
            _currentRotationY = 0.0f;
            transformRotatable.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Throw(Vector3 velocity, Vector3 angularVelocity)
        {
            IEnumerator Coroutine()
            {
                for (var loopLimit = 0; loopLimit < int.MaxValue; loopLimit++)
                {
                    var position = transform.position;
                    position += velocity * Time.deltaTime;
                    transform.position = position;

                    var eulerAngles = transform.eulerAngles;
                    eulerAngles += angularVelocity * Time.deltaTime;
                    transform.eulerAngles = eulerAngles;
                    
                    yield return null;
                }
            }

            _coroutineThrow = StartCoroutine(Coroutine());
        }

        public void ThrowReset()
        {
            if (_coroutineThrow != null)
            {
                StopCoroutine(_coroutineThrow);
            }
            
            transform.eulerAngles = Vector3.zero;
        }
    }
}
